using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName =("Input/InputReader"))]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction<Vector2, bool> Look = delegate { };
    public event UnityAction EnableMouseControlCamera = delegate { };
    public event UnityAction DisableMouseControlCamera = delegate { };
    public event UnityAction<bool> Jump = delegate { };
    public event UnityAction<bool> Sprint = delegate { };
    public event UnityAction<bool> Crouching = delegate { };

    PlayerInput inputActions;

    public Vector3 direction => inputActions.Player.Move.ReadValue<Vector2>();

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInput();
            inputActions.Player.SetCallbacks(this);
        }
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    public void EnablePlayerActions()
    {
        inputActions.Enable();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Jump.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                Jump.Invoke(false);
                break;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

    bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
    public void OnMove(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnMouseControlCamera(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                EnableMouseControlCamera();
                break;
            case InputActionPhase.Canceled:
                DisableMouseControlCamera();
                break;    
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Sprint.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                Sprint.Invoke(false);
                break;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Crouching.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                Crouching.Invoke(false);
                break;
        }
    }
}
