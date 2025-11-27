using UnityEngine;

public class HardStoppingState : StoppingState
{
    public HardStoppingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.decelerationForce = m_Player.config.hardDecelerationForce;
        m_Player.attrs.currentJumpForce = m_Player.config.strongJumpForce;
    }
}

