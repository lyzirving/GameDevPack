using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Custom/Character/Player/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Base Movement")]
    [Range(0f, 25f)]
    public float baseSpeed = 3f;

    [Range(0f, 1f)]
    public float walkSpeedModifer = 0.4f;

    [Range(1f, 2f)]
    public float runSpeedModifer = 1f;    

    [Range(0f, 1f)]
    public float reachTargetRotationTime = 0.075f;

    [Header("Slope Data")]
    [SerializeField]
    public AnimationCurve slopeSpeedCurve;

    #region Dash Data
    [Header("Dashing Data")]
    [Range(1f, 3f)]
    public float dashSpeedModifer = 2f;

    [Tooltip("Time interval between two dash")]
    [Range(0f, 2f)]
    public float consecutiveDashTime = 1f;

    [Range(0f, 5)]
    public int consecutiveDashLimit = 2;

    [Range(0f, 5f)]
    public float dashLimitCooldown = 1.75f;

    [Range(0f, 2f)]
    public float dashDuration = 0.5f;
    #endregion

    [Header("Sprinting Data")]
    [Range(1f, 3f)]
    public float sprintSpeedModifer = 1.6f;

    [Tooltip("Transition time from sprint to run since sprint is canceled")]
    [Range(0f, 5f)]
    public float sprintToRunTime = 1f;

    [Tooltip("Transition time from run to walk since walk is canceled")]
    [Range(0f, 2f)]
    public float runToWalkTime = 0.5f;
}
