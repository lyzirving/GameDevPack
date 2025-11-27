using System;
using UnityEngine;

public class StoppingState : GroundedState
{
    public StoppingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.speedModifier = 0f;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        RotateTowardsTargetRotation();

        if (!IsMoveHorizontally())
            return;

        DecelerateHorizontally();
    }    

    public override void OnAnimationTransitionEvent()
    {
        m_StateMachine.ChangeState(m_StateMachine.idlingState);
    }
    #endregion

    #region Input Method
    protected override void AddInputAction()
    {
        base.AddInputAction();
        InputManager.instance.actions.PlayerInput.Movement.started += OnMoveStart;
    }    

    protected override void RemoveInputAction() 
    {
        base.RemoveInputAction();
        InputManager.instance.actions.PlayerInput.Movement.started -= OnMoveStart;
    }

    protected override void OnMoveStart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnMove();
    }
    #endregion
}
