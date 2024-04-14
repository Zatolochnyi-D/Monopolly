using System;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager Instance;

    [SerializeField] private EndGameUI endGameScreen;

    private PlayerLogic winner;

    public PlayerLogic Winner => winner;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        TurnManager.Instance.OnTurnEnded += DeclareWinner;
    }

    public void DeclareWinner()
    {
        endGameScreen.Show();
    }

    public void SetWinner(PlayerLogic player)
    {
        winner = player;
    }

    [ContextMenu("InstantWin")]
    private void InstantWin()
    {
        winner = TurnManager.Instance.CurrentPlayer;
        DeclareWinner();
    }
}
