using System;
using MovementSystem;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine m_StateMachine;
    protected Vector2 m_Move2d;
    protected float m_Speed = 5f;
    protected float m_SpeedModifier = 1f;

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
        ReadMovement();
    }    

    public virtual void Update()
    {
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }
    
    #endregion

    #region Main Methods
    protected void ReadMovement()
    {
        m_Move2d = m_StateMachine.player.input.actions.PlayerInput.Movement.ReadValue<Vector2>();
    }

    protected void Move()
    {
        if (m_Move2d == Vector2.zero || Mathf.Approximately(m_SpeedModifier, 0f))
        {
            Debug.Log("returned");
            return;
        }
        Vector3 direction = GetMovementDirection();
        float speed = GetMovementSpeed();

        Vector3 curVelocity = GetHorizontalVelocity();
        m_StateMachine.player.rdBody.AddForce(speed * direction - curVelocity, ForceMode.VelocityChange);
    }    
    #endregion

    #region Reusable Methods
    protected Vector3 GetMovementDirection()
    {        
        return new Vector3(m_Move2d.x, 0f, m_Move2d.y);
    }

    protected float GetMovementSpeed()
    {
        return m_Speed * m_SpeedModifier;
    }

    protected Vector3 GetHorizontalVelocity()
    {
        var velocity = m_StateMachine.player.rdBody.linearVelocity;
        velocity.y = 0f;
        return velocity;
    }
    #endregion
}
