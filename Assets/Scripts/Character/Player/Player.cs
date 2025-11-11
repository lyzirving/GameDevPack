using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    public PlayerInput input { get; private set; }
    public Rigidbody rdBody { get; private set; }

    private PlayerMovementStateMachine m_MoveStateMachine;

    private void Awake()
    {
        m_MoveStateMachine = new PlayerMovementStateMachine(this);        
        // adjust Rigidbody's params in inspector as below:
        // 1) Freeze all rotation. 2) Make Collision Detection Continuous. 3) Make Interpolate be Interpolate
        rdBody = GetComponent<Rigidbody>();

        input = GetComponent<PlayerInput>();     
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
