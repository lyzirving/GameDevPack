using UnityEngine;


namespace MovementSystem
{
    public abstract class StateMachine
    {
        public IState currentState { get; private set; }

        public void ChangeState(IState newState)
        {
            if (currentState == newState)
            {
                Debug.LogWarning($"incoming state[{newState}] equals current state[{currentState}]");
                return;
            }

            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        public void HandleInput()
        {
            currentState?.HandleInput();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void PhsicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }        
    }
}
