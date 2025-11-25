using System;
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

    protected override void OnWalkToggle(InputAction.CallbackContext context)
    {
        base.OnWalkToggle(context);
        m_StateMachine.ChangeState(m_StateMachine.walkingState);
    }

    #region Main Method
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
