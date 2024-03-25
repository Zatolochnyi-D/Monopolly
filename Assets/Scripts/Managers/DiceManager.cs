using System;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    public event Action OnRollDiceTriggered;

    [SerializeField] private Button diceRollButton;

    void Awake()
    {
        Instance = this;

        diceRollButton.onClick.AddListener(() =>
        {
            OnRollDiceTriggered?.Invoke();
        });

        // TODO: bind hotkey for dice rolling
    }
}
