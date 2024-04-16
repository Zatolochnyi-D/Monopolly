using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance { get; private set; }

    public event Action OnThrowDicePerformed;
    public event Action OnOpenCloseStatsPreformed;
    public event Action OnPausePerformed;
    public event Action OnUnpausePerformed;

    private InputActions inputActions;

    public bool IsPaused => inputActions.Pause.enabled;

    void Awake()
    {
        Instance = this;

        inputActions = new();

        inputActions.Game.Enable();
        inputActions.Game.ThrowDice.performed += ThrowDicePerformed;
        inputActions.Game.OpenCloseStats.performed += OpenCloseStatsPreformed;
        inputActions.Game.Pause.performed += GamePausePerformed;

        inputActions.Pause.Unpause.performed += PauseUnpausePerformed;
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

    private void GamePausePerformed(InputAction.CallbackContext context)
    {
        OnPausePerformed?.Invoke();
    }

    private void PauseUnpausePerformed(InputAction.CallbackContext context)
    {
        OnUnpausePerformed?.Invoke();
    }

    public void Disable()
    {
        inputActions.Disable();
    }

    public void ToPause()
    {
        inputActions.Game.Disable();
        inputActions.Pause.Enable();
    }

    public void FromPause()
    {
        inputActions.Pause.Disable();
        inputActions.Game.Enable();
    }
}
