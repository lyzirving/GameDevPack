using UnityEngine;

/// <summary>
/// DashState can be transit from Idle state, all movement state, all stopping state and light landing state.
/// </summary>
public class DashState : GroundedState
{
    private float m_StartTime;
    private int m_ConsecutiveDashUsed;    

    public DashState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Method
    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.speedModifier = m_Player.config.dashSpeedModifer;

        Dash();
        UpdateConsecutiveDashes();
        m_StartTime = Time.time;
    }

    public override void OnAnimationTransitionEvent()
    {
        base.OnAnimationTransitionEvent();

        if (!HasMovementInput())
        {
            m_StateMachine.ChangeState(m_StateMachine.idlingState);
            return;
        }
        m_StateMachine.ChangeState(m_StateMachine.sprintingState);
    }

    #endregion

    #region Input Method
    protected override void OnStartDash(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Do nothing
    }
    #endregion

    #region Main Method
    private void Dash()
    {
        var dashDirection = m_Player.transform.forward;
        dashDirection.y = 0f;

        if (m_StateMachine.lastState.IsStationary())
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

        if (m_ConsecutiveDashUsed >= m_Player.config.consecutiveDashLimit)
        {
            m_ConsecutiveDashUsed = 0;
            InputManager.instance.DisableActionForTime(InputManager.instance.actions.PlayerInput.Dash, m_Player.config.dashLimitCooldown); 
        }
    }

    private bool IsConsecutive()
    {
        return Time.time < m_StartTime +  m_Player.config.consecutiveDashTime;
    }
    #endregion
}
