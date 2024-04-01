using System;
using System.Threading.Tasks;
using UnityEngine;

public partial class PlayerLogic : MonoBehaviour
{
    public event Action OnMovementEnded;

    // data
    private int tileID;
    private string playerName;
    private PawnVisualsSO playerVisuals;
    private int number;
    private int money;
    private int image;
    private int product = 0;
    private int passiveIncome = 0;
    // TODO: shares

    private TileLogic currentTile;
    private float delayBetweenSteps = 0.2f;

    public string PlayerName => playerName;
    public Color DisplayColor => playerVisuals.displayColor;
    public int Money => money;
    public int Number => number;
    public int Image => image;

    void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerVisuals.visual;

        currentTile = MapManager.Instance.FindTileByID(tileID);
        transform.position = currentTile.TakePlace(this);

        TileLogic.PositionUpdated += OnPositionUpdated;
        TurnManager.Instance.OnTurnStarted += MovePlayer;
    }

    private void OnPositionUpdated(int id)
    {
        if (id == currentTile.TileID)
        {
            transform.position = currentTile.GetUpdatedPosition(this);
        }
    }

    public void MovePlayer(int rolledNumber)
    {
        if (TurnManager.Instance.CurrentPlayer == this)
        {
            currentTile.ReleasePlace(this);

            MoveStepByStep(rolledNumber);
        }
    }

    private async void MoveStepByStep(int steps)
    {
        if (steps != 0)
        {
            currentTile = currentTile.NextTile;
            transform.position = currentTile.GetTemporalPosition();
            await Task.Delay((int)(delayBetweenSteps * 1000));
            MoveStepByStep(steps - 1);
        }
        else
        {
            transform.position = currentTile.TakePlace(this);
            currentTile.AlterPlayer(this);
            OnMovementEnded?.Invoke();
        }
    }
}
