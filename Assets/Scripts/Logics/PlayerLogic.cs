using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public partial class PlayerLogic : MonoBehaviour
{
    public interface IHandler
    {
        public IHandler NextHandler { get; set; }
        public void Handle(PlayerLogic player);
    }


    public class DebtCheck : IHandler
    {
        private IHandler next;
        private PlayerCommand moveCommand;

        public IHandler NextHandler { get => next; set => next = value; }

        public DebtCheck()
        {
            moveCommand = new MovePlayerCommand()
            {
                Parameters = new SimpleTileParam() { tile = MapManager.Instance.FindTileByID(0) },
            };
        }

        public void Handle(PlayerLogic player)
        {
            Debug.Log("Debt Check");

            if (player.money < 0)
            {
                moveCommand.TargetPlayer = player;
                moveCommand.Execute();
            }

            next?.Handle(player);
        }
    }


    public class EarlyWinCheck : IHandler
    {
        private IHandler next;

        public IHandler NextHandler { get => next; set => next = value; }

        public void Handle(PlayerLogic player)
        {
            Debug.Log("Early Win Check");

            if (player.Directors.Sum(x => x.power) >= 9)
            {
                EndGameManager.Instance.SetWinner(player);
                return;
            }

            next?.Handle(player);
        }
    }


    public struct PlayerShares
    {
        public int Airlines;
        public int CarManufacturer;
        public int TourismAgency;
        public int TVCompany;
        public int BuildingAgency;
        public int BookPublisher;

        public static PlayerShares operator +(PlayerShares a, PlayerShares b)
        {
            return new()
            {
                Airlines = a.Airlines + b.Airlines,
                CarManufacturer = a.CarManufacturer + b.CarManufacturer,
                TourismAgency = a.TourismAgency + b.TourismAgency,
                TVCompany = a.TVCompany + b.TVCompany,
                BuildingAgency = a.BuildingAgency + b.BuildingAgency,
                BookPublisher = a.BookPublisher + b.BookPublisher,
            };
        }
    }


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
    private PlayerShares playerShares = new();
    private ObservableCollection<Director> playerDirectors = new();

    private TileLogic currentTile;
    private readonly float delayBetweenSteps = 0.2f;

    private IHandler HandleOnTurnStart;
    private IHandler HandleOnTurnEnd;

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
    public PlayerShares Shares
    {
        get => playerShares;
        private set
        {
            playerShares = value;
            OnPropertyChanged?.Invoke();
        }
    }
    public ObservableCollection<Director> Directors
    {
        get => playerDirectors;
    }

    void Awake()
    {
        HandleOnTurnStart = new DebtCheck();

        HandleOnTurnEnd = new DebtCheck()
        {
            NextHandler = new EarlyWinCheck()
        };
    }

    void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerVisuals.visual;

        currentTile = MapManager.Instance.FindTileByID(tileID);
        transform.position = currentTile.TakePlace(this);

        TileLogic.PositionUpdated += OnPositionUpdated;
        TurnManager.Instance.OnTurnStarted += MovePlayer;
        TenderInteractionUI.OnPlayerEnterTender += GetProduction;
        TurnManager.Instance.OnNewTurn += PerformInitialChecks;
        TurnManager.Instance.OnTurnEnded += PerformFinalChecks;

        playerDirectors.CollectionChanged += (sender, args) => OnPropertyChanged?.Invoke();
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

    private void PerformInitialChecks()
    {
        if (TurnManager.Instance.CurrentPlayer == this)
        {
            HandleOnTurnStart.Handle(this);
        }
    }

    private void PerformFinalChecks()
    {
        if (TurnManager.Instance.CurrentPlayer == this)
        {
            HandleOnTurnEnd.Handle(this);
        }
    }

    [ContextMenu("Bankrupt")]
    private void Bankrupt()
    {
        money -= 9999;
    }

    [ContextMenu("ConjureDirectors")]
    private void ConjureDirectors()
    {
        playerDirectors.Add(new Director() {name = "Athos", power = 100});
        playerDirectors.Add(new Director() { name = "Porthos", power = 100 });
        playerDirectors.Add(new Director() { name = "Aramis", power = 100 });
    }
}
