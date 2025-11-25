using System;
using UnityEngine;

public class GroundedState : PlayerBaseState
{
    public GroundedState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.slopeSpeedModifier = 1f;
    }

    public override void Update()
    {
        if (!HasMovementInput())
        {
            m_StateMachine.ChangeState(m_StateMachine.idlingState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Float();
    }

    #region Input Method
    protected override void AddInputAction()
    {
        base.AddInputAction();
        InputManager.instance.actions.PlayerInput.Dash.started += OnDashPerform;
        InputManager.instance.actions.PlayerInput.Dash.canceled += OnDashCancel;
    }    

    protected override void RemoveInputAction()
    {
        base.RemoveInputAction();
        InputManager.instance.actions.PlayerInput.Dash.started -= OnDashPerform;
        InputManager.instance.actions.PlayerInput.Dash.canceled -= OnDashCancel;
    }

    protected virtual void OnDashPerform(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_StateMachine.ChangeState(m_StateMachine.dashState);
    }

    protected virtual void OnDashCancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {        
    }
    #endregion

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
}
