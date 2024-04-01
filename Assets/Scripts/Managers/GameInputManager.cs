using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance { get; private set; }

    public event Action OnThrowDicePerformed;
    public event Action OnOpenCloseStatsPreformed;

    private InputActions inputActions;

    void Awake()
    {
        Instance = this;

        inputActions = new();

        inputActions.Game.Enable();
        inputActions.Game.ThrowDice.performed += ThrowDicePerformed;
        inputActions.Game.OpenCloseStats.performed += OpenCloseStatsPreformed;
    }

    void OnDestroy()
    {
        inputActions.Dispose();
    }

    private void ThrowDicePerformed(InputAction.CallbackContext context)
    {
        OnThrowDicePerformed?.Invoke();
    }

    private void OpenCloseStatsPreformed(InputAction.CallbackContext context)
    {
        OnOpenCloseStatsPreformed?.Invoke();
    }
}
