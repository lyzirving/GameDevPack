using System;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    public Vector2 move2d;
    public float speed;
    public float speedModifier;

    public float targetRotationY;
    public float dumpedTargetRotationPassTime;
    public float dumpedTargetRotationCurrentVelocity;
    public float timeToReachTargetRotation;

    public PlayerData(float inSpeed)
    {
        speed = inSpeed;
        speedModifier = 1f;
        move2d = Vector2.zero;

        targetRotationY = 0f;
        dumpedTargetRotationPassTime = 0f;
        dumpedTargetRotationCurrentVelocity = 0f;
        timeToReachTargetRotation = 0.075f;
    }
}
