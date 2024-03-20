using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    [SerializeField] private PlayerMovementLogic[] players;
    private int currentPlayerIndex = 0;

    public PlayerMovementLogic CurrentPlayer => players[currentPlayerIndex];

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        DiceManager.Instance.DiceRolled += OnDiceRolled;
    }

    private void OnDiceRolled(int rolledNumber)
    {
        CurrentPlayer.MovePlayer(rolledNumber);

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
    }
}
