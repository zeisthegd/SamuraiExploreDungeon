using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Game Logic/ Input Reader", fileName = "Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IDialogueActions
{
    //Gameplay
    public event UnityAction chargeBegin;
    public event UnityAction chargeEnd;
    public event UnityAction dodgeEvent;
    public event UnityAction sprintEvent;
    public event UnityAction attackEvent;
    public event UnityAction<Vector2> moveEvent;

    private GameInput gameInput;

    //UI
    public UnityAction interactEvent;

    void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
            gameInput.Gameplay.SetCallbacks(this);
            gameInput.Dialogue.SetCallbacks(this);
        }
        EnableGameplayInput();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnCharge(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                chargeBegin?.Invoke();
                break;
            case InputActionPhase.Canceled:
                chargeEnd?.Invoke();
                break;
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (sprintEvent != null)
            sprintEvent.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (dodgeEvent != null)
            dodgeEvent.Invoke();
    }

    public void OnAccept(InputAction.CallbackContext context)
    {

    }
    public void OnMouseLook(InputAction.CallbackContext context)
    {

    }

    void EnableGameplayInput()
    {
        gameInput.Gameplay.Enable();
        gameInput.Dialogue.Disable();
    }
    void EnableDialogueInput()
    {
        gameInput.Gameplay.Disable();
        gameInput.Dialogue.Enable();
    }

    void DisableAllInputs()
    {
        gameInput.Gameplay.Disable();
        gameInput.Dialogue.Disable();
    }


}
