using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public event Action TurnPassed;

    [SerializeField] private PlayerMovementLogic[] players;

    private PlayerStatsLogic[] playersData;
    private int currentPlayerIndex = 0;

    public PlayerMovementLogic CurrentPlayer => players[currentPlayerIndex];
    public PlayerStatsLogic CurrentPlayerData => playersData[currentPlayerIndex];

    void Awake()
    {
        Instance = this;

        playersData = new PlayerStatsLogic[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            playersData[i] = players[i].GetComponent<PlayerStatsLogic>();
        }
    }

    void Start()
    {
        DiceManager.Instance.DiceRolled += OnDiceRolled;
    }

    private void OnDiceRolled(int rolledNumber)
    {
        CurrentPlayer.MovePlayer(rolledNumber);

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        TurnPassed?.Invoke();
    }
}
