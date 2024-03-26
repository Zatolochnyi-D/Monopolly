using System;
using UnityEngine;

public class DiceThrowingManager : MonoBehaviour
{
    public static DiceThrowingManager Instance;

    public event Action<int> OnDiceMovementStopped;
    public event Action<int> OnDiceReset;

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
    }

    private void ResetDice()
    {
        firstDice.Reset();
        secondDice.Reset();

        OnDiceReset?.Invoke(totalRolledNumber);

        totalRolledNumber = 0;
    }

    void Start()
    {
        firstDice.OnMovementStopped += CalculateRolledNumber;
        secondDice.OnMovementStopped += CalculateRolledNumber;

        DiceManager.Instance.OnRollDiceTriggered += ThrowDices;
    }

    private void CalculateRolledNumber(int rolledNumber)
    {
        totalRolledNumber += rolledNumber;

        if (firstDice.IsStopped && secondDice.IsStopped)
        {
            OnDiceMovementStopped?.Invoke(totalRolledNumber);
            // start despawn countdown

            timer.Start();
        }
    }

    private void ThrowDices()
    {
        firstDice.Throw();
        secondDice.Throw();
    }
}
