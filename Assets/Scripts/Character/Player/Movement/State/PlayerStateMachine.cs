using MovementSystem;

public class PlayerStateMachine : StateMachine
{
    public Player player { get; private set; }

    public IdlingState idlingState { get; private set; }
    public WalkingState walkingState { get; private set; }
    public RunningState runningState { get; private set; }
    public DashState dashState { get; private set; }
    public SprintingState sprintingState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        this.player = player;

        idlingState = new IdlingState(this);
        walkingState = new WalkingState(this);
        runningState = new RunningState(this);
        dashState = new DashState(this);
        sprintingState = new SprintingState(this);
    }
}
