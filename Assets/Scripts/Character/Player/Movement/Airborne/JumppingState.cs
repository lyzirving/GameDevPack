using System;
using UnityEngine;

public class JumppingState : AirborneState
{
    private bool m_KeepRotate;

    public JumppingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.speedModifier = 0f;
        m_Player.attrs.reachTargetRotationTime = m_Player.config.jumpReachTargetRotationTime;
        m_Player.attrs.decelerationForce = m_Player.config.jumpDecelerationForce;
        m_KeepRotate = HasMovementInput();

        Jump();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(m_KeepRotate)
        {
            RotateTowardsTargetRotation();
        }

        if (IsMovingUp())
        {
            // Make player go to the top faster and less floaty
            DecelerateVertically();
        }
    }
    #endregion

    #region Input Methods
    #endregion

    #region Main Methods
    private void Jump()
    {
        Vector3 jumpForce = m_Player.attrs.currentJumpForce;
        Vector3 jumpDirection = m_Player.transform.forward;

        if (m_KeepRotate)
        {
            jumpDirection = GetRotationDirection(m_Player.attrs.targetRotationY);
        }

        jumpForce.x *= jumpDirection.x;
        jumpForce.z *= jumpDirection.z;

        CalcJumpForceOnSlope(ref jumpForce);
        ResetVelocity();

        m_Player.rdBody.AddForce(jumpForce, ForceMode.VelocityChange);
    }

    private void CalcJumpForceOnSlope(ref Vector3 jumpForce)
    {
        var center = m_Player.resizableCapsule.CenterInWordSpace();
        Ray ray = new Ray(center, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, m_Player.config.jumpToGroundRayDistance, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore))
        {
            float angle = Vector3.Angle(hitInfo.normal, -ray.direction);

            if (IsMovingUp())
            {
                float forceModier = m_Player.config.jumpForceModifierUpwards.Evaluate(angle);
                jumpForce.x *= forceModier;
                jumpForce.z *= forceModier;
            }
            else if (IsMovingDown())
            {
                float forceModier = m_Player.config.jumpForceModifierDownwards.Evaluate(angle);
                jumpForce.y *= forceModier;
            }
        }
    }
    #endregion
}
