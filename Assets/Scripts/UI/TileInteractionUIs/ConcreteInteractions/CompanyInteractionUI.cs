using System.Data.SqlTypes;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CompanyInteractionUI : InteractionUI
{
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
    [SerializeField] private int companyIndex;
    [SerializeField] private int costPerShare;
    [SerializeField] private int productionPerShare;

    private int availableShares = 100;
    private PlayerLogic player;

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
        playerCommand.NextCommand.Parameters = new PlayerLogic.DoubleIntegerParam() { first = companyIndex, second = shares };
        playerCommand.NextCommand.NextCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = production };
    }
}
