using System;
using UnityEngine;

public class DiceThrowingManager : MonoBehaviour
{
    public static DiceThrowingManager Instance;

    [SerializeField] private DiceLogic firstDice;
    [SerializeField] private DiceLogic secondDice;

    private int totalRolledNumber = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        firstDice.OnMovementStopped += CalculateRolledNumber;
        secondDice.OnMovementStopped += CalculateRolledNumber;

        DiceManager.Instance.DiceRolled += ThrowDices;
    }

    private void CalculateRolledNumber(int rolledNumber)
    {
        totalRolledNumber = rolledNumber;

        if (firstDice.IsStopped && secondDice.IsStopped)
        {
            // start despawn countdown
        }
    }

    private void ThrowDices()
    {
        firstDice.Throw();
        secondDice.Throw();
    }
}
