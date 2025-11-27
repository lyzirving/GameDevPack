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
    }
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

        ResetVelocity();

        m_Player.rdBody.AddForce(jumpForce, ForceMode.VelocityChange);
    }
    #endregion
}
