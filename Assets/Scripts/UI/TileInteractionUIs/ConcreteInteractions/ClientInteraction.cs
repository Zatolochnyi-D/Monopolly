using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClientInteraction : Interaction
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private GameObject rolledPlayerTexts;
    [SerializeField] private TextMeshProUGUI rolledPlayerNumberText;

    [SerializeField] private GameObject tradeScreen;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI victimNameText;
    [SerializeField] private Button playerRollButton;
    [SerializeField] private Button victimRollButton;
    [SerializeField] private TextMeshProUGUI playerRolledText;
    [SerializeField] private TextMeshProUGUI victimRolledText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button endTradeButton;

    [SerializeField] private GameObject noTradeScreen;
    [SerializeField] private Button noTradeButton;

    private NumberRoller rollerSelecter;
    private NumberRoller rollerForPlayer;
    private NumberRoller rollerForVictim;

    private PlayerLogic player;
    private PlayerLogic victim;

    private bool isPlayerRolled = false;
    private bool isVictimRolled = false;

    private int playerRolled;
    private int victimRolled;

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            SelectVictim();
        });

        cancelButton.onClick.AddListener(() =>
        {
            Close();
        });

        playerRollButton.onClick.AddListener(() =>
        {
            RollForPlayer();
        });

        victimRollButton.onClick.AddListener(() =>
        {
            RollForVictim();
        });

        endTradeButton.onClick.AddListener(() =>
        {
            Close();
        });

        noTradeButton.onClick.AddListener(() =>
        {
            Close();
        });
        rollerForPlayer = new(playerRolledText, Enumerable.Range(1, 6).ToArray());
        rollerForVictim = new(victimRolledText, Enumerable.Range(1, 6).ToArray());

        playerCommand = new PlayerLogic.AlterBalanceCommand()
        {
            NextCommand = new PlayerLogic.AlterProductionCommand()
            {
                NextCommand = new PlayerLogic.AlterBalanceCommand()
            }
        };

        Hide();
    }

    void Start()
    {
        rollerSelecter = new(rolledPlayerNumberText, TurnManager.Instance.PlayerNumbersExcludeCurrentPlayer);
    }

    private void Reset()
    {
        rolledPlayerTexts.SetActive(false);
        playerRollButton.gameObject.SetActive(true);
        victimRollButton.gameObject.SetActive(true);
        endTradeButton.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        isPlayerRolled = false;
        isVictimRolled = false;
        tradeScreen.SetActive(false);
        noTradeScreen.SetActive(false);
    }

    public override void Interact()
    {
        player = TurnManager.Instance.CurrentPlayer;
        playerCommand.TargetPlayer = player;
        playerCommand.NextCommand.TargetPlayer = player;

        Reset();

        if (player.Production < 100)
        {
            noTradeScreen.SetActive(true);
        }

        Show();
    }

    private async void SelectVictim()
    {
        rolledPlayerTexts.SetActive(true);

        int number = await rollerSelecter.Roll();
        victim = TurnManager.Instance.GetPlayerByNumber(number);
        playerCommand.NextCommand.NextCommand.TargetPlayer = victim;

        playerNameText.text = $"<#{player.DisplayColor.ToHexString()}>{player.PlayerName}</color>";
        victimNameText.text = $"<#{victim.DisplayColor.ToHexString()}>{victim.PlayerName}</color>";
        tradeScreen.SetActive(true);
    }

    private async void RollForPlayer()
    {
        playerRollButton.gameObject.SetActive(false);

        playerRolled = await rollerForPlayer.Roll();

        isPlayerRolled = true;

        CheckBothRolls();
    }

    private async void RollForVictim()
    {
        victimRollButton.gameObject.SetActive(false);

        victimRolled = await rollerForVictim.Roll();

        isVictimRolled = true;

        CheckBothRolls();
    }

    private void CheckBothRolls()
    {
        if (isPlayerRolled && isVictimRolled)
        {
            endTradeButton.gameObject.SetActive(true);
            resultText.gameObject.SetActive(true);

            float multiplyer = 1.0f + (player.Image + playerRolled - (victim.Number + victimRolled)) / 10.0f;
            int moneyToPay = Mathf.RoundToInt(player.Production * multiplyer / 100.0f);

            if (moneyToPay < 0)
            {
                resultText.text = $"{playerNameText.text} failed to sell production.";
                return;
            }

            resultText.text = $"{victimNameText.text} owes to {playerNameText.text} {moneyToPay}00$";
            SetParameters(moneyToPay, player.Production);
            playerCommand.Execute();
        }
    }

    private void SetParameters(int moneyToTransfer, int productionToBurn)
    {
        playerCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = moneyToTransfer };
        playerCommand.NextCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = -productionToBurn };
        playerCommand.NextCommand.NextCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = -moneyToTransfer };
    }
}
