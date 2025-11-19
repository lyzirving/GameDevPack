using MovementSystem;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public Player player { get; private set; }

    public IdlingState idlingState { get; private set; }
    public WalkingState walkingState { get; private set; }
    public RunningState runningState { get; private set; }
    public SprintingState sprintingState { get; private set; }

    public PlayerMovementStateMachine(Player player)
    {
        this.player = player;

        idlingState = new IdlingState(this);
        walkingState = new WalkingState(this);
        runningState = new RunningState(this);
        sprintingState = new SprintingState(this);
    }
}
