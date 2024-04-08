using System;
using System.Threading.Tasks;
using UnityEngine;

public partial class PlayerLogic : MonoBehaviour
{
    public static event Action OnPropertyChanged;

    public event Action OnMovementEnded;

    // data
    private int tileID;
    private string playerName;
    private PawnVisualsSO playerVisuals;
    private int number;
    private int money;
    private int image;
    private int production = 0;
    private int passiveProduct = 0;
    // TODO: shares

    private TileLogic currentTile;
    private readonly float delayBetweenSteps = 0.2f;

    public string PlayerName => playerName;
    public Color DisplayColor => playerVisuals.displayColor;
    public int Number => number;

    public int Money
    {
        get => money;
        private set
        {
            money = value;
            OnPropertyChanged?.Invoke();
        }
    }
    public int Image
    {
        get => image;
        private set
        {
            image = value;
            OnPropertyChanged?.Invoke();
        }
    }
    public int Production
    {
        get => production;
        private set
        {
            production = value;
            OnPropertyChanged?.Invoke();
        }
    }
    public int PassiveProduct
    {
        get => passiveProduct;
        private set
        {
            passiveProduct = value;
            OnPropertyChanged?.Invoke();
        }
    }

    void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerVisuals.visual;

        currentTile = MapManager.Instance.FindTileByID(tileID);
        transform.position = currentTile.TakePlace(this);

        TileLogic.PositionUpdated += OnPositionUpdated;
        TurnManager.Instance.OnTurnStarted += MovePlayer;
        TenderInteractionUI.OnPlayerEnterTender += GetProduction;
    }

    private void GetProduction()
    {
        production += passiveProduct;
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
            TakeTile();
            currentTile.Interact();
        }
    }

    private void TakeTile()
    {
        transform.position = currentTile.TakePlace(this);
        OnMovementEnded?.Invoke();
    }
}
