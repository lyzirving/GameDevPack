using UnityEngine;

public class AirborneState : PlayerBaseState
{
    public AirborneState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region Virtual Methods
    protected override void OnContactGround(Collider collider)
    {
        m_StateMachine.ChangeState(m_StateMachine.idlingState);
    }
    #endregion
}
