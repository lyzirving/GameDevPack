using MovementSystem;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine m_StateMachine;

    public PlayerMovementState(PlayerMovementStateMachine stateMachine)
    {
        m_StateMachine = stateMachine;
    }

    #region IState Methods
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
    }
    #endregion
}
