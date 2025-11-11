using UnityEngine;


namespace MovementSystem
{
    public abstract class StateMachine
    {
        protected IState m_CurrentState;

        public void ChangeState(IState newState)
        {
            m_CurrentState?.Exit();
            m_CurrentState = newState;
            m_CurrentState?.Enter();
        }

        public void HandleInput()
        {
            m_CurrentState?.HandleInput();
        }

        public void Update()
        {
            m_CurrentState?.Update();
        }

        public void PhsicsUpdate()
        {
            m_CurrentState?.PhysicsUpdate();
        }        
    }
}
