using System;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    public event Action OnDiceThrowed;
    public event Action<int> OnDiceLanded;
    public event Action<int> OnDiceReset;

    [SerializeField] private Button diceRollButton;
    [SerializeField] private DiceLogic firstDice;
    [SerializeField] private DiceLogic secondDice;

    private int totalRolledNumber = 0;
    private float timeToStartMovement = 1.0f;
    private AsyncTimer timer;

    void Awake()
    {
        Instance = this;

        timer = new(timeToStartMovement);
        timer.OnTimeOut += ResetDice;

        diceRollButton.onClick.AddListener(() =>
        {
            ThrowDice();
            OnDiceThrowed?.Invoke();
        });

        // TODO: bind hotkey for dice rolling
    }

    void Start()
    {
        firstDice.OnDiceLanded += CalculateRolledNumber;
        secondDice.OnDiceLanded += CalculateRolledNumber;
    }

    private void CalculateRolledNumber(int rolledNumber)
    {
        totalRolledNumber += rolledNumber;

        if (firstDice.IsStopped && secondDice.IsStopped)
        {
            OnDiceLanded?.Invoke(totalRolledNumber);
            timer.Start();
        }
    }

    private void ThrowDice()
    {
        firstDice.Throw();
        secondDice.Throw();
    }

    private void ResetDice()
    {
        firstDice.Reset();
        secondDice.Reset();

        OnDiceReset?.Invoke(totalRolledNumber);

        totalRolledNumber = 0;
    }
}
