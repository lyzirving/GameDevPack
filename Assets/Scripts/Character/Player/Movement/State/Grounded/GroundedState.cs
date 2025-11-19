using UnityEngine;

public class GroundedState : PlayerMovementState
{
    public GroundedState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Update()
    {
        if (!HasMovementInput())
        {
            m_StateMachine.ChangeState(m_StateMachine.idlingState);
            return;
        }
    }
}
