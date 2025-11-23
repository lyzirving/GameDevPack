using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform m_FollowTarget;

    [SerializeField]
    private float m_HorizontalRotationSpeed = 0.3f;
    [SerializeField]
    private float m_VerticalRotationSpeed = 0.12f;
    // limit camera's vertical movement
    [SerializeField]
    private float m_BottomClamp = -40f;
    // limit camera's vertical movement
    [SerializeField]
    private float m_TopClamp = 70f;

    private float m_CinemachineTargetPitch = 0f;
    private float m_CinemachineTargetYaw = 0f;

    private void LateUpdate()
    {
        CameraLogic();
    }

    private void CameraLogic()
    {
        if(m_FollowTarget == null || !InputManager.Instance.isEnabled)
            return;
        
        var input = InputManager.Instance.actions.PlayerInput.CameraMove.ReadValue<Vector2>();
        m_CinemachineTargetPitch = UpdateRotation(m_CinemachineTargetPitch, input.y, m_BottomClamp, m_TopClamp, true, m_VerticalRotationSpeed);
        m_CinemachineTargetYaw = UpdateRotation(m_CinemachineTargetYaw, input.x, float.MinValue, float.MaxValue, false, m_HorizontalRotationSpeed);

        ApplyRotations(m_CinemachineTargetPitch, m_CinemachineTargetYaw);
    }

    private float UpdateRotation(float current, float input, float min, float max, bool isXAxis, float speed)
    {
        current += (isXAxis ? -input : input) * speed;
        return Mathf.Clamp(current, min, max);
    }

    private void ApplyRotations(float pitch, float yaw)
    {        
        m_FollowTarget.rotation = Quaternion.Euler(pitch, yaw, m_FollowTarget.eulerAngles.z);
    }
}
