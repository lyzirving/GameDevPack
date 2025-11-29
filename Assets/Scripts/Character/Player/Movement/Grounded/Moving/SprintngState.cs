using UnityEngine;
using UnityEngine.InputSystem;

public class SprintingState : GroundedState
{   
    private bool m_KeepSprint;
    private bool m_ShouldResetSprintingState;

    public SprintingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {       
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();
        m_KeepSprint = false;
        m_ShouldResetSprintingState = true;
        m_Player.attrs.speedModifier = m_Player.config.sprintSpeedModifer;
        m_Player.attrs.currentJumpForce = m_Player.config.strongJumpForce;
    }

    public override void Exit()
    {
        base.Exit();        
        if (m_ShouldResetSprintingState)
        {
            m_KeepSprint = false;
            m_Player.attrs.shouldSprint = false;
        }        
    }

    public override void Update()
    {
        base.Update();

        if (m_KeepSprint)
            return;
        
        if(Time.time < m_StartTime + m_Player.config.sprintToRunTime)
            return;

        StopSprinting();
    }
    #endregion 

    #region Input Method
    protected override void AddInputAction()
    {
        base.AddInputAction();
        InputManager.instance.actions.PlayerInput.Sprint.started += OnPerformSprint;
        InputManager.instance.actions.PlayerInput.Sprint.canceled += OnCancelSprint;
    }    

    protected override void RemoveInputAction() 
    {
        base.RemoveInputAction();
        InputManager.instance.actions.PlayerInput.Sprint.started -= OnPerformSprint;
        InputManager.instance.actions.PlayerInput.Sprint.canceled -= OnCancelSprint;
    }

    protected override void OnMoveCancel(InputAction.CallbackContext context)
    {
        m_StateMachine.ChangeState(m_StateMachine.hardStoppingState);
    }

    protected override void OnJumpStart(InputAction.CallbackContext context)
    {
        // shouldn't reset sprinting state before jump
        m_ShouldResetSprintingState = false;
        base.OnJumpStart(context);
    }

    private void OnPerformSprint(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_KeepSprint = true;
        m_Player.attrs.shouldSprint = true;
    }

    private void OnCancelSprint(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_KeepSprint = false;
    }
    #endregion

    #region Main Methods
    private void StopSprinting()
    {
        if (!HasMovementInput())
        {
            m_StateMachine.ChangeState(m_StateMachine.idlingState);
        }
        else
        {
            m_StateMachine.ChangeState(m_StateMachine.runningState);
        }
    }
    #endregion
}
