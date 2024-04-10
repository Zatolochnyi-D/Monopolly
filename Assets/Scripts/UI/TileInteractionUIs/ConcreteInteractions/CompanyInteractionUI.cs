using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompanyInteractionUI : InteractionUI
{
    public interface ISharesFactory
    {
        public PlayerLogic.PlayerShares Create(int shares);
    }


    public class AirlinesSharesFactory : ISharesFactory
    {
        public PlayerLogic.PlayerShares Create(int shares)
        {
            return new() { Airlines = shares };
        }
    }


    public class CarManufacturerSharesFactory : ISharesFactory
    {
        public PlayerLogic.PlayerShares Create(int shares)
        {
            return new() { CarManufacturer = shares };
        }
    }


    public class TourismAgencySharesFactory : ISharesFactory
    {
        public PlayerLogic.PlayerShares Create(int shares)
        {
            return new() { TourismAgency = shares };
        }
    }


    public class TVCompanySharesFactory : ISharesFactory
    {
        public PlayerLogic.PlayerShares Create(int shares)
        {
            return new() { TVCompany = shares };
        }
    }


    public class BuildingAgencySharesFactory : ISharesFactory
    {
        public PlayerLogic.PlayerShares Create(int shares)
        {
            return new() { BuildingAgency = shares };
        }
    }


    public class BookPublisherSharesFactory : ISharesFactory
    {
        public PlayerLogic.PlayerShares Create(int shares)
        {
            return new() { BookPublisher = shares };
        }
    }


    public enum SharesType
    {
        Airlines,
        CarManufacturer,
        TourismAgency,
        TVCompany,
        BuildingAgency,
        BookPublisher
    }


    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI availableText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI productionText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI totalCost;
    [SerializeField] private GameObject warning;

    [SerializeField] private GameObject noShares;
    [SerializeField] private Button noSharesButton;

    [SerializeField] private GameObject afterBuy;
    [SerializeField] private Button afterBuyButton;

    [SerializeField] private string companyName;
    [SerializeField] private SharesType companyType;
    [SerializeField] private int costPerShare;
    [SerializeField] private int productionPerShare;

    private int availableShares = 100;
    private PlayerLogic player;
    private ISharesFactory sharesFactory;

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Buy();
        });

        cancelButton.onClick.AddListener(() =>
        {
            Close();
        });

        inputField.onValueChanged.AddListener((string value) => 
        {
            ValidateInput(value);
            UpdateTotalCost(value);
        });

        noSharesButton.onClick.AddListener(() =>
        {
            Close();
        });

        afterBuyButton.onClick.AddListener(() =>
        {
            Close();
        });

        playerCommand = new PlayerLogic.AlterBalanceCommand()
        {
            NextCommand = new PlayerLogic.AlterPlayerShares()
            {
                NextCommand = new PlayerLogic.AlterProductionCommand()
            }
        };

        switch (companyType)
        {
            case SharesType.Airlines:
                sharesFactory = new AirlinesSharesFactory();
                break;
            case SharesType.CarManufacturer:
                sharesFactory = new CarManufacturerSharesFactory();
                break;
            case SharesType.TourismAgency:
                sharesFactory = new TourismAgencySharesFactory();
                break;
            case SharesType.TVCompany:
                sharesFactory = new TVCompanySharesFactory();
                break;
            case SharesType.BuildingAgency:
                sharesFactory = new BuildingAgencySharesFactory();
                break;
            case SharesType.BookPublisher:
                sharesFactory = new BookPublisherSharesFactory();
                break;
        }
    }

    public override void Interact()
    {
        player = TurnManager.Instance.CurrentPlayer;
        playerCommand.TargetPlayer = player;
        playerCommand.NextCommand.TargetPlayer = player;
        playerCommand.NextCommand.NextCommand.TargetPlayer = player;

        warning.SetActive(false);
        noShares.SetActive(false);
        afterBuy.SetActive(false);

        if (availableShares == 0)
        {
            noShares.SetActive(true);
            Show();
            return;
        }

        availableText.text = $"{companyName} offers {availableShares} to buy.";
        costText.text = $"Cost: {costPerShare}";
        productionText.text = $"Production: {productionPerShare}";
        totalCost.text = $"Total: {costPerShare * 10}00$";
        inputField.text = "10";

        Show();
    }

    private void Buy()
    {
        int shares = int.Parse(inputField.text);
        int total = shares * costPerShare;

        if (total > player.Money)
        {
            warning.SetActive(true);
            return;
        }

        SetParameters(total, shares, shares * productionPerShare);
        playerCommand.Execute();
        availableShares -= shares;

        afterBuy.SetActive(true);
    }

    private void Close()
    {
        Hide();
        EndTurn();
    }

    private void UpdateTotalCost(string value)
    {
        warning.SetActive(false);
        totalCost.text = $"Total: {costPerShare * int.Parse(inputField.text)}00$";
    }

    private void ValidateInput(string value)
    {
        if (value == "")
        {
            value = "0";
            inputField.text = value;
        }
        int inputShares = int.Parse(value);
        if (inputShares < 0)
        {
            inputField.text = "0";
        }

        if (inputShares > availableShares)
        {
            inputField.text = $"{availableShares}";
        }
    }

    private void SetParameters(int cost, int shares, int production)
    {
        playerCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = -cost };
        playerCommand.NextCommand.Parameters = new PlayerLogic.SimpleSharesParam() { shares = sharesFactory.Create(shares) };
        playerCommand.NextCommand.NextCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = production };
    }
}
