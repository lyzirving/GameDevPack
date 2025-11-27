using UnityEngine;

public class MediumStoppingState : StoppingState
{
    public MediumStoppingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.decelerationForce = m_Player.config.mediumDecelerationForce;
        m_Player.attrs.currentJumpForce = m_Player.config.mediumJumpForce;
    }
}
