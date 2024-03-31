using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public event Action OnTurnEnded;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PawnVisualsSO[] pawnVisuals;

    private PlayerLogic[] players;
    private int currentPlayerIndex = 0;

    public PlayerLogic CurrentPlayer => players[currentPlayerIndex];

    void Awake()
    {
        Instance = this;

        players = new PlayerLogic[4];

        List<PlayerLogic.PlayerBuilder> builders = new();

        string[] names = new[]
        {
            "Uranium",
            "Ferum",
            "Cobalt",
            "Zirconium"
        };

        // create test players
        for (int i = 0; i < 4; i++)
        {
            builders.Add(new(playerPrefab));
            builders[i].SetPosition(0);
            builders[i].SetName(names[i]);
            builders[i].SetNumber(5);
            builders[i].SetVisuals(pawnVisuals[i]);
        }

        for (int i = 0; i < players.Length; i++)
        {
            players[i] = builders[i].GetProduct().GetComponent<PlayerLogic>();
        }
    }

    void Start()
    {
        DiceManager.Instance.OnDiceThrowingEnded += MoveCurrentPlayer;
    }

    private void MoveCurrentPlayer(int rolledNumber)
    {
        CurrentPlayer.MovePlayer(rolledNumber);

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        OnTurnEnded?.Invoke();
    }
}
