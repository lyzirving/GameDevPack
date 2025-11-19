using UnityEngine.InputSystem;

/// <summary>
/// WalkingState can transition to Running, LightStopping, Dashing and Jumpping State.
/// </summary>
public class WalkingState : GroundedState
{
    public WalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {      
    }

    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.speedModifier = m_Player.config.walkSpeedModifer;
    }    

    protected override void OnWalkToggle(InputAction.CallbackContext context)
    {
        base.OnWalkToggle(context);
        m_StateMachine.ChangeState(m_StateMachine.runningState);
    }
}
