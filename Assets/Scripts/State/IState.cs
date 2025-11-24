using UnityEngine;

namespace MovementSystem
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void HandleInput();
        public void Update();
        public void PhysicsUpdate();
        public bool IsStationary();
        public void OnAnimationEnterEvent();
        public void OnAnimationExitEvent();
        public void OnAnimationTransitionEvent();
    }
}
