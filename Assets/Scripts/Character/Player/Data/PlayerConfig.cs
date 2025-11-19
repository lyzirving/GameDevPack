using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Custom/Character/Player/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{    
    [Range(0f, 25f)]
    public float baseSpeed = 3f;

    [Range(0f, 1f)]
    public float walkSpeedModifer = 0.4f;

    [Range(1f, 2f)]
    public float runSpeedModifer = 1f;

    [Range(0f, 1f)]
    public float reachTargetRotationTime = 0.075f;
}
