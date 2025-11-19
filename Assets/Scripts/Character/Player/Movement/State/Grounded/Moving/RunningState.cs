using UnityEngine.InputSystem;

/// <summary>
/// RunningState can transite to Walk, Medium Stopping, Dashing and Jumpping.
/// </summary>
public class RunningState : GroundedState
{
    public RunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.speedModifier = m_Player.config.runSpeedModifer;
    }

    protected override void OnWalkToggle(InputAction.CallbackContext context)
    {
        base.OnWalkToggle(context);
        m_StateMachine.ChangeState(m_StateMachine.walkingState);
    }
}
