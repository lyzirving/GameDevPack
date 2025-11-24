using UnityEngine;

[RequireComponent(typeof(PlayerCameraControl))]
public class Player : MonoBehaviour
{
    [Header("Config")]
    public PlayerConfig config;

    public Transform cameraTransform;

    public PlayerAttrs attrs = new PlayerAttrs();

    public Rigidbody rdBody { get; private set; }
    public PlayerCameraControl cameraControl { get; private set; }

    public ResizableCapsuleCollider resizableCapsule { get; private set; }

    private PlayerStateMachine m_MoveStateMachine;

    private void Awake()
    {
        if (config == null)
        {
            throw new System.Exception("Player Config hasn't been set yet!");
        }

        // adjust Rigidbody's params in inspector as below:
        // 1) Freeze all rotation. 2) Make Collision Detection Continuous. 3) Make Interpolate be Interpolate
        rdBody = GetComponent<Rigidbody>();

        cameraControl = GetComponent<PlayerCameraControl>();

        cameraTransform = Camera.main.transform;

        resizableCapsule = GetComponent<ResizableCapsuleCollider>();

        m_MoveStateMachine = new PlayerStateMachine(this);       
    }

    private void Start()
    {                      
        m_MoveStateMachine.ChangeState(m_MoveStateMachine.idlingState);
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
