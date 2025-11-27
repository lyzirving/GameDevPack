using UnityEngine;


namespace MovementSystem
{
    public abstract class StateMachine
    {
        public IState currentState { get; private set; }
        public IState lastState { get; private set; }

        public void ChangeState(IState newState)
        {
            if (currentState == newState)
            {
                Debug.LogWarning($"incoming state[{newState}] equals current state[{currentState}]");
                return;
            }

            currentState?.Exit();
            lastState = currentState;
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

        public void OnAnimationEnterEvent()
        {
            currentState?.OnAnimationEnterEvent();
        }

        public void OnAnimationExitEvent()
        {
            currentState?.OnAnimationExitEvent();
        }

        public void OnAnimationTransitionEvent()
        { 
            currentState?.OnAnimationTransitionEvent();
        }

        public void OnTriggerEnter(Collider collider)
        { 
            currentState?.OnTriggerEnter(collider);
        }
    }
}
