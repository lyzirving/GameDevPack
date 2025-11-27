using UnityEngine.InputSystem;

/// <summary>
/// WalkingState can transition to Running, LightStopping, Dashing and Jumpping State.
/// </summary>
public class WalkingState : GroundedState
{
    public WalkingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {      
    }

    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.speedModifier = m_Player.config.walkSpeedModifer;
        m_Player.attrs.currentJumpForce = m_Player.config.weakJumpForce;
    }

    #region Input Method
    protected override void OnMoveCancel(InputAction.CallbackContext context)
    {
        m_StateMachine.ChangeState(m_StateMachine.lightStoppingState);
    }

    protected override void OnWalkToggle(InputAction.CallbackContext context)
    {
        base.OnWalkToggle(context);
        m_StateMachine.ChangeState(m_StateMachine.runningState);
    }
    #endregion
}
