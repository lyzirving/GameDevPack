using MovementSystem;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected Player m_Player;
    protected PlayerStateMachine m_StateMachine;
    protected float m_StartTime;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        m_StateMachine = stateMachine;
        m_Player = stateMachine.player;
    }

    #region IState Methods
    public virtual void Enter()
    {        
        Debug.Log($"{GetType().Name} enter");
        AddInputAction();
        m_StartTime = Time.time;
    }    

    public virtual void Exit()
    {
        RemoveInputAction();
    }    

    public virtual void HandleInput()
    {
        if (!InputManager.instance.isEnabled)
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

    public virtual bool IsStationary()
    {
        return false;
    }

    public virtual void OnAnimationEnterEvent()
    { 
    }

    public virtual void OnAnimationExitEvent()
    { 
    }

    public virtual void OnAnimationTransitionEvent()
    { 
    }
    #endregion

    #region Input Method
    protected virtual void AddInputAction()
    {
        InputManager.instance.actions.PlayerInput.WalkToggle.started += OnWalkToggle;
    }

    protected virtual void RemoveInputAction()
    {
        InputManager.instance.actions.PlayerInput.WalkToggle.started -= OnWalkToggle;
    }

    protected virtual void OnWalkToggle(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_Player.attrs.shouldRun = !m_Player.attrs.shouldRun;
    }
    #endregion

    #region Main Methods
    protected void ReadMovement()
    {
        m_Player.attrs.move2d = InputManager.instance.actions.PlayerInput.Movement.ReadValue<Vector2>();
    }

    protected void Move()
    {
        if (m_Player.attrs.move2d == Vector2.zero || Mathf.Approximately(m_Player.attrs.speedModifier, 0f))
            return;

        Vector3 direction = GetInputMovement();

        float angle = Rotate(direction);
        Vector3 targetDir = GetRotationDirection(angle);

        float speed = GetMovementSpeed();

        Vector3 curVelocity = GetHorizontalVelocity();
        m_StateMachine.player.rdBody.AddForce(speed * targetDir - curVelocity, ForceMode.VelocityChange);
    }

    protected float Rotate(Vector3 direction)
    {
        float angle = UpdateTargetRotation(direction, true);
        RotateTowardsTargetRotation();
        return angle;
    }

    protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
    {
        float angle = direction.GetHorizontalAngle();

        if (shouldConsiderCameraRotation)
            angle = AddCameraRotationToAngle(angle);

        if (!Mathf.Approximately(angle, m_Player.attrs.targetRotationY))
        {
            // set target rotation angle
            m_Player.attrs.targetRotationY = angle;
            m_Player.attrs.dumpedTargetRotationPassTime = 0f;
        }
        return angle;
    }

    protected void RotateTowardsTargetRotation()
    {
        float currentY = m_Player.rdBody.rotation.eulerAngles.y;
        if (Mathf.Approximately(currentY, m_Player.attrs.targetRotationY))
            return;

        float smoothAngle = Mathf.SmoothDampAngle(currentY, m_Player.attrs.targetRotationY,
            ref m_Player.attrs.dumpedTargetRotationCurrentVelocity,
            m_Player.config.reachTargetRotationTime);

        m_Player.attrs.dumpedTargetRotationPassTime += Time.deltaTime;
        var targetRot = Quaternion.Euler(0f, smoothAngle, 0f);
        m_Player.rdBody.MoveRotation(targetRot);
    }

    protected float AddCameraRotationToAngle(float angle)
    {
        angle += m_StateMachine.player.cameraTransform.eulerAngles.y;
        if(angle > 360f)
            angle -= 360f;
        return angle;
    }

    protected void ResetVelocity()
    {
        m_Player.rdBody.linearVelocity = Vector3.zero;        
    }    

    protected bool HasVelocity()
    {
        return m_Player.rdBody.linearVelocity != Vector3.zero;
    }

    protected bool HasMovementInput()
    {
        return m_Player.attrs.move2d != Vector2.zero;
    }

    #endregion

    #region Reusable Methods
    protected Vector3 GetInputMovement()
    {        
        return new Vector3(m_Player.attrs.move2d.x, 0f, m_Player.attrs.move2d.y);
    }

    protected float GetMovementSpeed(bool shouldConsiderSlopes = true)
    {
        float speed = m_Player.config.baseSpeed * m_Player.attrs.speedModifier;
        if (shouldConsiderSlopes)
        {
            speed *= m_Player.attrs.slopeSpeedModifier;
        }
        return speed;     
    }

    protected Vector3 GetHorizontalVelocity()
    {
        var velocity = m_StateMachine.player.rdBody.linearVelocity;
        velocity.y = 0f;
        return velocity;
    }

    protected Vector3 GetPlayerVerticalVelocity()
    {
        return new Vector3(0f, m_Player.rdBody.linearVelocity.y, 0f);
    }

    protected Vector3 GetRotationDirection(float targetRotationAngle)
    {
        return Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;
    }
    #endregion
}
