using UnityEngine;

public class LightStoppingState : StoppingState
{
    public LightStoppingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.decelerationForce = m_Player.config.lightDecelerationForce;
        m_Player.attrs.currentJumpForce = m_Player.config.weakJumpForce;
    }
}
