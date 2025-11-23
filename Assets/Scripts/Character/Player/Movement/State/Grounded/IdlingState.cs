/// <summary>
/// IdlingState can transite to Walking, Running, Dashing and Jumpping state.
/// </summary>
public class IdlingState : GroundedState
{
    public IdlingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {               
    }

    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.speedModifier = 0f;
        ResetVelocity();
    }

    public override void Update()
    {
        if (HasMovementInput())
        {
            m_StateMachine.ChangeState(m_Player.attrs.shouldRun ? m_StateMachine.runningState : m_StateMachine.walkingState);
            return;
        }
    }
}
