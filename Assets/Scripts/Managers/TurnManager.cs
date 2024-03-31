using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static PlayerLogic.PlayerBuilder[] playerBuilders; // assume this field filled at the start of the game
    public static TurnManager Instance { get; private set; }

    public event Action<int> OnTurnStarted;
    public event Action OnTurnEnded;

    [SerializeField] private GameObject playerPrefab;

    private PlayerLogic[] players;
    private int currentPlayerIndex = 0;

    public PlayerLogic CurrentPlayer => players[currentPlayerIndex];

    void Awake()
    {
        Instance = this;

        players = new PlayerLogic[playerBuilders.Length];
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = playerBuilders[i].GetProduct(playerPrefab).GetComponent<PlayerLogic>();
        }
    }

    void Start()
    {
        DiceManager.Instance.OnDiceReset += DoTurn;
    }

    private void DoTurn(int rolledNumber)
    {
        OnTurnStarted?.Invoke(rolledNumber);

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        OnTurnEnded?.Invoke();
    }
}
