using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
  private PlayerInput _controls;
  public event Action OnJumpEvent;
  public event Action OnDashEvent;
  public event Action<Vector2> OnMoveEvent;
  public event Action OnAttackEvent;

  void Awake()
  {
    _controls = new PlayerInput();
    _controls.Player.Enable();
    _controls.Player.Jump.performed += OnJump;
    _controls.Player.Dash.performed += OnDash;
    _controls.Player.Movement.performed += OnMove;
    _controls.Player.Movement.canceled += OnMove;
    _controls.Player.Attack.performed += OnAttack;

  }

    void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnAttackEvent?.Invoke();
            Debug.Log("Attack event triggered");
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJumpEvent?.Invoke();
        }
    }

    void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnDashEvent?.Invoke();
        }
    }

    void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        OnMoveEvent?.Invoke(moveInput);
    }

    void OnDisable()
    {
        _controls.Player.Jump.performed -= OnJump;
        _controls.Player.Dash.performed -= OnDash;
        _controls.Player.Movement.performed -= OnMove;
        _controls.Player.Movement.canceled -= OnMove;
        _controls.Player.Attack.performed -= OnAttack;
        _controls.Player.Disable();
    }



}
