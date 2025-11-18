using System;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : PlayerMovementState
{
    public IdleState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {               
    }

    public override void Enter()
    {
        base.Enter();
        m_Player.data.speedModifier = 0f;
        ResetVelocity();
    }

    protected override bool CheckStateChange(out MovementSystem.IState newState)
    {        
        if (IsMoving())
        { 
            newState = m_Player.data.shouldRun ? m_StateMachine.runState : m_StateMachine.walkState;
            return true;
        }
        newState = null;
        return false;
    }
}
