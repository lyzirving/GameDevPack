using UnityEngine;

public struct PlayerAttrs
{
    public Vector2 move2d;
    public Vector3 currentJumpForce;
    public float reachTargetRotationTime;

    public float speedModifier;
    public float slopeSpeedModifier;
    public float decelerationForce;

    public float targetRotationY;
    public float dumpedTargetRotationPassTime;
    public float dumpedTargetRotationCurrentVelocity;

    public bool shouldRun;
    public bool shouldSprint;
}

