using MovementSystem;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public Player player { get; private set; }

    public IdleState idleState { get; private set; }
    public WalkState walkState { get; private set; }
    public RunState runState { get; private set; }
    public SprintState sprintState { get; private set; }

    public PlayerMovementStateMachine(Player player)
    {
        this.player = player;

        idleState = new IdleState(this);

        walkState = new WalkState(this);
        runState = new RunState(this);
        sprintState = new SprintState(this);
    }
}
