using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance { get; private set; }

    public event Action OnThrowDicePerformed;

    private InputActions inputActions;

    void Awake()
    {
        Instance = this;

        inputActions = new();

        inputActions.Game.Enable();
        inputActions.Game.ThrowDice.performed += ThrowDicePerformed;
    }

    void OnDestroy()
    {
        inputActions.Dispose();
    }

    private void ThrowDicePerformed(InputAction.CallbackContext context)
    {
        OnThrowDicePerformed?.Invoke();
    }
}
