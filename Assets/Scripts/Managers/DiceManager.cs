using System;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    public event Action DiceRolled;

    [SerializeField] private Button diceRollButton;

    void Awake()
    {
        Instance = this;

        diceRollButton.onClick.AddListener(() =>
        {
            RollDice();
        });

        // TODO: bind hotkey for dice rolling
    }

    private void RollDice()
    {
        DiceRolled?.Invoke();
    }
}
