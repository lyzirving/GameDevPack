using System;
using UnityEngine;

/// <summary>
/// DashingState can be transit from Idle state, all movement state, all stopping state and light landing state.
/// </summary>
public class DashingState : GroundedState
{
    private int m_ConsecutiveDashUsed;
    private float m_LastStartTime;
    private bool m_ShouldKeepRotate = false;

    public DashingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        m_LastStartTime = m_StartTime;
        base.Enter();
        m_Player.attrs.speedModifier = m_Player.config.dashSpeedModifer;
        m_Player.attrs.currentJumpForce = m_Player.config.strongJumpForce;
        m_ShouldKeepRotate = HasMovementInput();

        Dash();
        UpdateConsecutiveDashes();
    }

    public override void Update()
    {
        if(Time.time < m_StartTime + m_Player.config.dashDuration)
            return;

        StopDashing();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void OnAnimationTransitionEvent()
    {       
        if (!HasMovementInput())
        {
            m_StateMachine.ChangeState(m_StateMachine.hardStoppingState);
            return;
        }
        m_StateMachine.ChangeState(m_StateMachine.sprintingState);
    }
    #endregion

    #region Input Method
    protected override void OnMoveStart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_ShouldKeepRotate = true;
    }

    protected override void OnMoveCancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_ShouldKeepRotate = false;
    }

    protected override void OnDashPerform(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Should do nothing
    }
    #endregion

    #region Main Methods
    private void Dash()
    {
        var dashDirection = m_Player.transform.forward;
        dashDirection.y = 0f;

        if (!HasMovementInput())
        {
            UpdateTargetRotation(dashDirection, false);
        }
        else
        {
            UpdateTargetRotation(GetInputMovement());
            dashDirection = GetRotationDirection(m_Player.attrs.targetRotationY);
        }    
        m_Player.rdBody.linearVelocity = dashDirection * GetMovementSpeed(false);
    }

    private void UpdateConsecutiveDashes()
    {
        if (!IsConsecutive())
            m_ConsecutiveDashUsed = 0;

        ++m_ConsecutiveDashUsed;

        if (m_ConsecutiveDashUsed > m_Player.config.consecutiveDashLimit)
        {
            m_ConsecutiveDashUsed = 0;
            InputManager.instance.DisableActionForTime(InputManager.instance.actions.PlayerInput.Dash, m_Player.config.dashLimitCooldown); 
        }
    }

    private bool IsConsecutive()
    {
        return Time.time < m_LastStartTime +  m_Player.config.consecutiveDashTime;
    }

    private void StopDashing()
    {
        if (!HasMovementInput())
        {
            m_StateMachine.ChangeState(m_StateMachine.idlingState);
        }
        else 
        {
            m_StateMachine.ChangeState(m_StateMachine.runningState);
        }
    }
    #endregion
}
