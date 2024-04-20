using System;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public class GameSnapshot : IMemento
    {
        public string[] serializedPlayers;

        public GameSnapshot(TurnManager turnManager)
        {
            serializedPlayers = turnManager.players.Select(x => Saver.SerializePlayer(x.CreateSnapshot())).ToArray();
        }
    }

    public static PlayerLogic.PlayerBuilder[] playerBuilders; // assume this field filled at the start of the game
    public static TurnManager Instance { get; private set; }

    public enum TurnStates
    {
        TurnStarted,
        InteractionStarted,
    }

    public event Action<int> OnTurnStarted;
    public event Action OnTurnEnded;
    public event Action OnNewTurn;

    [SerializeField] private GameObject playerPrefab;

    private PlayerLogic[] players;
    private int currentPlayerIndex = 0;
    private TurnStates turnState = TurnStates.TurnStarted;

    public PlayerLogic CurrentPlayer => players[currentPlayerIndex];
    public int HighestImage => players.Max(x => x.Image);
    public int HighestImageExcludeCurrentPlayer => players.Where(x => x != CurrentPlayer).Max(x => x.Image);
    public int[] PlayerNumbers => players.Select(x => x.Number).ToArray();
    public int[] PlayerNumbersExcludeCurrentPlayer => players.Where(x => x != CurrentPlayer).Select(x => x.Number).ToArray();

    void Awake()
    {
        Instance = this;

        players = new PlayerLogic[playerBuilders.Length];
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = playerBuilders[i].GetProduct(playerPrefab).GetComponent<PlayerLogic>();
            players[i].transform.parent = transform;
        }
    }

    void Start()
    {
        DiceManager.Instance.OnDiceReset += DoTurn;
    }

    private void DoTurn(int rolledNumber)
    {
        turnState = TurnStates.InteractionStarted;
        OnTurnStarted?.Invoke(rolledNumber);
    }

    public void EndTurn()
    {
        OnTurnEnded?.Invoke();

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        EndGameManager.Instance.DeclareWinner();

        OnNewTurn?.Invoke();
    }

    public PlayerLogic GetPlayerByNumber(int number)
    {
        return players.Where(x => x.Number == number).First();
    }

    public GameSnapshot CreateSnapshot()
    {
        return new(this);
    }
}
