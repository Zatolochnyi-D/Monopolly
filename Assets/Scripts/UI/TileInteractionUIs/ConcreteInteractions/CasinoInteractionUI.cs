using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CasinoInteractionUI : InteractionUI
{
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI luckyNumberText;
    [SerializeField] private TMP_InputField betField;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI rolledNumberText;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI endScreenSummaryText;
    [SerializeField] private Button confirmEndButton;

    private int firstLuckyNumber;
    private int secondLuckyNumber;

    private PlayerLogic.AlterBalanceCommand playerCommand = new();

    void Awake()
    {
        cancelButton.onClick.AddListener(() =>
        {
            Close();
        });

        confirmButton.onClick.AddListener(() =>
        {
            Play();
        });

        confirmEndButton.onClick.AddListener(() =>
        {
            Close();
        });

        ResetDialog();

        betField.onValueChanged.AddListener((string value) => warningText.gameObject.SetActive(false));
    }

    private void ResetDialog()
    {
        warningText.gameObject.SetActive(false);
        rolledNumberText.gameObject.SetActive(false);
        endScreen.SetActive(false);
    }

    public override void Iteract(PlayerLogic player)
    {
        currentPlayer = player;

        firstLuckyNumber = Random.Range(1, 7);
        secondLuckyNumber = Random.Range(1, 7);
        while (secondLuckyNumber == firstLuckyNumber)
        {
            secondLuckyNumber = Random.Range(1, 7);
        }
        luckyNumberText.text = $"Today lucky numbers are {firstLuckyNumber} and {secondLuckyNumber}.\nMake your bet (100$ and more):";

        Show();
    }

    private void Close()
    {
        currentPlayer = null;
        ResetDialog();
        Hide();
        EndTurn();
    }

    private async void Play()
    {
        string betText = betField.text;

        if (betText.Length < 3) return;

        int bet = int.Parse(betField.text[0..^2]);

        if (currentPlayer.Money < bet) 
        {
            warningText.gameObject.SetActive(true);
            return;
        }

        rolledNumberText.gameObject.SetActive(true);

        bool isVictorious = await Roll();

        endScreen.SetActive(true);
        if (isVictorious)
        {
            endScreenSummaryText.text = $"You won! \n+{bet * 2}00$";
            playerCommand.balance = bet * 2;
        }
        else
        {
            endScreenSummaryText.text = $"You lost. \n-{bet}00$";
            playerCommand.balance = -bet;
        }
        playerCommand.Execute(currentPlayer);
    }

    private async Task<bool> Roll()
    {
        int rolledNumber = 0;
        for (int i = 0; i < 20; i++)
        {
            rolledNumber = Random.Range(1, 7);
            rolledNumberText.text = rolledNumber.ToString();
            await Task.Delay(100);
        }

        await Task.Delay(1000);

        return rolledNumber == firstLuckyNumber || rolledNumber == firstLuckyNumber;
    }
}
