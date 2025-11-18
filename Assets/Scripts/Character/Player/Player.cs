using UnityEngine;

[RequireComponent(typeof(PlayerCameraControl))]
public class Player : MonoBehaviour
{
    [Header("Attribute")]
    public PlayerData data = new PlayerData(3f);
    public Transform cameraTransform;    

    public Rigidbody rdBody { get; private set; }
    public PlayerCameraControl cameraControl { get; private set; }    

    private PlayerMovementStateMachine m_MoveStateMachine;

    private void Awake()
    {
        // adjust Rigidbody's params in inspector as below:
        // 1) Freeze all rotation. 2) Make Collision Detection Continuous. 3) Make Interpolate be Interpolate
        rdBody = GetComponent<Rigidbody>();

        cameraControl = GetComponent<PlayerCameraControl>();

        cameraTransform = Camera.main.transform;

        m_MoveStateMachine = new PlayerMovementStateMachine(this);       
    }

    private void Start()
    {                      
        m_MoveStateMachine.ChangeState(m_MoveStateMachine.idleState);
    }

    private void Update()
    {
        m_MoveStateMachine.HandleInput();

        m_MoveStateMachine.Update();
    }

    private void FixedUpdate()
    {
        m_MoveStateMachine.PhsicsUpdate();
    }
}
