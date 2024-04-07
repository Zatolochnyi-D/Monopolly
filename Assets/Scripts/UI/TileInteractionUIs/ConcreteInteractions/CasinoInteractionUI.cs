using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
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

    private PlayerLogic currentPlayer;

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

        playerCommand = new PlayerLogic.AlterBalanceCommand();
    }

    private void ResetDialog()
    {
        warningText.gameObject.SetActive(false);
        rolledNumberText.gameObject.SetActive(false);
        endScreen.SetActive(false);
    }

    public override void Interact(PlayerLogic player)
    {
        playerCommand.TargetPlayer = player;
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

        PlayerLogic.SimpleIntegerParam intToAdd = new();
        if (isVictorious)
        {
            intToAdd.integer = bet * 2;
            endScreenSummaryText.text = $"You won! \n+{intToAdd.integer}00$";
        }
        else
        {
            intToAdd.integer = -bet;
            endScreenSummaryText.text = $"You lost. \n{intToAdd.integer}00$";
        }
        playerCommand.Parameters = intToAdd;
        playerCommand.Execute();
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
