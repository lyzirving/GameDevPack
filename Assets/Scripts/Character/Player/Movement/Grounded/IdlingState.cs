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
        m_Player.attrs.currentJumpForce = m_Player.config.stationaryJumpForce;
        ResetVelocity();
    }

    public override void Update()
    {
        if (!HasMovementInput())
            return;

        OnMove();
    }

    public override bool IsStationary()
    {
        return true;
    }
}
