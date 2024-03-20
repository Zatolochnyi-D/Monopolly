using System;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    public event Action<int> DiceRolled;

    [SerializeField] private Button rollDiceButton;

    // 2 x d6
    private readonly int minRolledNumber = 2;
    private readonly int maxRolledNumber = 12;

    void Awake()
    {
        Instance = this;

        rollDiceButton.onClick.AddListener(() =>
        {
            RollDice();
        });

        // TODO: bind hotkey for dice rolling
    }

    private void RollDice()
    {
        int rolledNumber = UnityEngine.Random.Range(minRolledNumber, maxRolledNumber + 1); // max is exclusive, so +1 to get possibility of 12
        DiceRolled?.Invoke(rolledNumber);
    }
}
