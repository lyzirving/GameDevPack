using System;
using MovementSystem;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected Player m_Player;
    protected PlayerMovementStateMachine m_StateMachine;

    public PlayerMovementState(PlayerMovementStateMachine stateMachine)
    {
        m_StateMachine = stateMachine;
        m_Player = stateMachine.player;
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
        if (!InputManager.Instance.isEnabled)
            return;

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
        m_Player.data.move2d = InputManager.Instance.actions.PlayerInput.Movement.ReadValue<Vector2>();
    }

    protected void Move()
    {
        if (m_Player.data.move2d == Vector2.zero || Mathf.Approximately(m_Player.data.speedModifier, 0f))
            return;

        Vector3 direction = GetInputMovement();

        float angle = Rotate(direction);
        Vector3 targetDir = GetRotationDirection(angle);

        float speed = GetMovementSpeed();

        Vector3 curVelocity = GetHorizontalVelocity();
        m_StateMachine.player.rdBody.AddForce(speed * targetDir - curVelocity, ForceMode.VelocityChange);
    }

    private float Rotate(Vector3 direction)
    {
        float angle = direction.GetHorizontalAngle();
        angle = AddCameraRotationToAngle(angle);

        if (!Mathf.Approximately(angle, m_Player.data.targetRotationY))
        {
            // set target rotation angle
            m_Player.data.targetRotationY = angle;
            m_Player.data.dumpedTargetRotationPassTime = 0f;
        }

        RotateTowardsTargetRotation();
        return angle;
    }

    private void RotateTowardsTargetRotation()
    {
        float currentY = m_Player.rdBody.rotation.eulerAngles.y;
        if (Mathf.Approximately(currentY, m_Player.data.targetRotationY))
            return;

        float smoothAngle = Mathf.SmoothDampAngle(currentY, m_Player.data.targetRotationY,
            ref m_Player.data.dumpedTargetRotationCurrentVelocity,
            m_Player.data.timeToReachTargetRotation);

        m_Player.data.dumpedTargetRotationPassTime += Time.deltaTime;
        var targetRot = Quaternion.Euler(0f, smoothAngle, 0f);
        m_Player.rdBody.MoveRotation(targetRot);
    }

    private float AddCameraRotationToAngle(float angle)
    {
        angle += m_StateMachine.player.cameraTransform.eulerAngles.y;
        if(angle > 360f)
            angle -= 360f;
        return angle;
    }

    #endregion

    #region Reusable Methods
    protected Vector3 GetInputMovement()
    {        
        return new Vector3(m_Player.data.move2d.x, 0f, m_Player.data.move2d.y);
    }

    protected float GetMovementSpeed()
    {
        return m_Player.data.speed * m_Player.data.speedModifier;
    }

    protected Vector3 GetHorizontalVelocity()
    {
        var velocity = m_StateMachine.player.rdBody.linearVelocity;
        velocity.y = 0f;
        return velocity;
    }

    protected Vector3 GetRotationDirection(float targetRotationAngle)
    {
        return Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;
    }
    #endregion
}
