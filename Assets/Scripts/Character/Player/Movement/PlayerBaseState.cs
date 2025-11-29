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
        Debug.Log($"enter {GetType().Name}");
        AddInputAction();
        m_StartTime = Time.time;
        m_Player.attrs.reachTargetRotationTime = m_Player.config.reachTargetRotationTime;
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

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (Utility.IsGroundLayer(collider.gameObject.layer))
        { 
            OnContactGround(collider);
        }
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

    #region Virtual Methods
    protected virtual void OnContactGround(Collider collider)
    { 
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

        Vector3 curVelocity = GetPlayerHorizontalVelocity();
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
            m_Player.attrs.reachTargetRotationTime - m_Player.attrs.dumpedTargetRotationPassTime);

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

    protected void DecelerateHorizontally()
    {
        var vel = GetPlayerHorizontalVelocity();
        m_Player.rdBody.AddForce(-vel * m_Player.attrs.decelerationForce, ForceMode.Acceleration);
    }

    protected void DecelerateVertically()
    {
        var vel = GetPlayerVerticalVelocity();
        m_Player.rdBody.AddForce(-vel * m_Player.attrs.decelerationForce, ForceMode.Acceleration);
    }

    protected void ResetVelocity()
    {
        m_Player.rdBody.linearVelocity = Vector3.zero;        
    }    

    protected bool IsMoveHorizontally(float miniMagnitude = 0.1f)
    {
        var vel = GetPlayerHorizontalVelocity();
        var vel2d = new Vector2(vel.x, vel.z);
        return vel2d.magnitude > miniMagnitude;
    }

    protected bool IsMovingUp(float miniMagnitude = 0.1f)
    { 
        return m_Player.rdBody.linearVelocity.y > miniMagnitude;
    }

    protected bool IsMovingDown(float miniMagnitude = 0.1f)
    {
        return m_Player.rdBody.linearVelocity.y < -miniMagnitude;
    }

    protected bool HasMovementInput()
    {
        return m_Player.attrs.move2d != Vector2.zero;
    }

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

    protected Vector3 GetPlayerHorizontalVelocity()
    {
        var velocity = m_Player.rdBody.linearVelocity;
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
