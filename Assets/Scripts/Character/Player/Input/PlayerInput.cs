using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputAction inputActions { get; private set; }
    public Vector2 moveInput { get; private set; }

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        inputActions.PlayerInput.Movement.performed += UpdateMovement;
        inputActions.PlayerInput.Movement.canceled += CancelMovement;
    }        

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    #region Main Methods
    private void UpdateMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void CancelMovement(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
    #endregion
}
