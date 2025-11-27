using UnityEngine;
using UnityEngine.InputSystem;

public class SprintingState : GroundedState
{   
    private bool m_KeepSprint = false;

    public SprintingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {       
    }

    public override void Enter()
    {
        base.Enter();
        m_Player.attrs.speedModifier = m_Player.config.sprintSpeedModifer;
        m_Player.attrs.currentJumpForce = m_Player.config.strongJumpForce;
    }

    public override void Exit()
    {
        base.Exit();
        m_KeepSprint = false;
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

    private void OnPerformSprint(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("OnPerformSprint");
        m_KeepSprint = true;
    }

    private void OnCancelSprint(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("OnCancelSprint");
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
