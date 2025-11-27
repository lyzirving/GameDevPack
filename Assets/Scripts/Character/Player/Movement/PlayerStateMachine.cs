using MovementSystem;

public class PlayerStateMachine : StateMachine
{
    public Player player { get; }
    public IdlingState idlingState { get; }
    public WalkingState walkingState { get; }
    public RunningState runningState { get; }
    public DashingState dashState { get; }
    public SprintingState sprintingState { get; }
    public LightStoppingState lightStoppingState { get; }
    public MediumStoppingState mediumStoppingState { get; }
    public HardStoppingState hardStoppingState { get; }

    public JumppingState jumppingState { get; }

    public PlayerStateMachine(Player player)
    {
        this.player = player;

        idlingState = new IdlingState(this);
        dashState = new DashingState(this);

        walkingState = new WalkingState(this);
        runningState = new RunningState(this);        
        sprintingState = new SprintingState(this);

        lightStoppingState = new LightStoppingState(this);
        mediumStoppingState = new MediumStoppingState(this);
        hardStoppingState = new HardStoppingState(this);

        jumppingState = new JumppingState(this);
    }
}
