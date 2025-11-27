using System;
using UnityEngine;

public class GroundedState : PlayerBaseState
{
    public GroundedState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.slopeSpeedModifier = 1f;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Float();
    }
    #endregion

    #region Input Method
    protected override void AddInputAction()
    {
        base.AddInputAction();
        InputManager.instance.actions.PlayerInput.Dash.started += OnDashPerform;
        InputManager.instance.actions.PlayerInput.Dash.canceled += OnDashCancel;

        InputManager.instance.actions.PlayerInput.Movement.started += OnMoveStart;
        InputManager.instance.actions.PlayerInput.Movement.canceled += OnMoveCancel;

        InputManager.instance.actions.PlayerInput.Jump.started += OnJumpStart;
    }    

    protected override void RemoveInputAction()
    {
        base.RemoveInputAction();
        InputManager.instance.actions.PlayerInput.Movement.started -= OnMoveStart;
        InputManager.instance.actions.PlayerInput.Movement.canceled -= OnMoveCancel;

        InputManager.instance.actions.PlayerInput.Dash.started -= OnDashPerform;
        InputManager.instance.actions.PlayerInput.Dash.canceled -= OnDashCancel;

        InputManager.instance.actions.PlayerInput.Jump.started -= OnJumpStart;
    }

    protected virtual void OnDashPerform(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_StateMachine.ChangeState(m_StateMachine.dashState);
    }

    protected virtual void OnDashCancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {        
    }

    protected virtual void OnMoveStart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    protected virtual void OnMoveCancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    private void OnJumpStart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_StateMachine.ChangeState(m_StateMachine.jumppingState);
    }
    #endregion

    #region Virtual Methods
    protected virtual void OnMove()
    {
        if (m_Player.attrs.shouldSprint)
        {
            m_StateMachine.ChangeState(m_StateMachine.sprintingState);
            return;
        }

        if (m_Player.attrs.shouldRun)
        {
            m_StateMachine.ChangeState(m_StateMachine.runningState);
            return;
        }

        m_StateMachine.ChangeState(m_StateMachine.walkingState);
    }
    #endregion

    #region Main Methods
    protected void Float()
    {
        Vector3 centerInWorldSpace = m_Player.resizableCapsule.colliderData.collider.bounds.center;
        var ray = new Ray(centerInWorldSpace, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, m_Player.resizableCapsule.slopeData.floatRayDistance, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore))
        {
            float groundAngle = Vector3.Angle(hit.normal, -ray.direction);

            float slopeSpeedModifier = SetSlopeSpeedByGroundAngle(groundAngle);

            if (Mathf.Approximately(slopeSpeedModifier, 0f))
                return;

            float distanceToFloatingPoint = m_Player.resizableCapsule.colliderData.centerInLocalSpace.y * m_Player.transform.localScale.y - hit.distance;
            if (Mathf.Approximately(distanceToFloatingPoint, 0f))
                return;

            float amountToLift = distanceToFloatingPoint * m_Player.resizableCapsule.slopeData.stepReachForce - GetPlayerVerticalVelocity().y;
            Vector3 liftForce = new Vector3(0f, amountToLift, 0f);
            m_Player.rdBody.AddForce(liftForce, ForceMode.VelocityChange);
        }
    }    

    private float SetSlopeSpeedByGroundAngle(float groundAngle)
    {
        float slopeSpeedModifier = m_Player.config.slopeSpeedCurve.Evaluate(groundAngle);

        if (!Mathf.Approximately(m_Player.attrs.slopeSpeedModifier, slopeSpeedModifier))
        {
            m_Player.attrs.slopeSpeedModifier = slopeSpeedModifier;
        }

        return slopeSpeedModifier;
    }
    #endregion
}
