using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// RunningState can transite to Walk, Medium Stopping, Dashing and Jumpping.
/// </summary>
public class RunningState : GroundedState
{
    public RunningState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.speedModifier = m_Player.config.runSpeedModifer;
        m_Player.attrs.currentJumpForce = m_Player.config.mediumJumpForce;
    }

    public override void Update()
    {
        base.Update();

        if (m_Player.attrs.shouldRun)
            return;

        if (Time.time < m_StartTime + m_Player.config.runToWalkTime)
            return;

        StopRunning();
    }

    #region Input Method
    protected override void OnMoveCancel(InputAction.CallbackContext context)
    {
        m_StateMachine.ChangeState(m_StateMachine.mediumStoppingState);
    }

    protected override void OnWalkToggle(InputAction.CallbackContext context)
    {
        base.OnWalkToggle(context);
        m_StateMachine.ChangeState(m_StateMachine.walkingState);
    }
    #endregion

    #region Main Methods
    private void StopRunning()
    {
        if (!HasMovementInput())
        {
            m_StateMachine.ChangeState(m_StateMachine.idlingState);
        }
        else 
        {
            m_StateMachine.ChangeState(m_StateMachine.walkingState);
        }
    }
    #endregion
}
