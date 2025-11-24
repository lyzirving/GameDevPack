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
    [Header("Dash Data")]
    [Range(1f, 3f)]
    public float dashSpeedModifer = 2f;

    [Range(0f, 2f)]
    public float consecutiveDashTime = 1f;

    [Range(0f, 5)]
    public int consecutiveDashLimit = 2;

    [Range(0f, 5f)]
    public float dashLimitCooldown = 1.75f;
    #endregion
}
