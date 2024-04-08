using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TenderInteractionUI : InteractionUI
{
    public static event Action OnPlayerEnterTender;

    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private GameObject warning;
    [SerializeField] private TextMeshProUGUI rolledNumberText;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI endScreenText;
    [SerializeField] private Button endScreenButton;

    [SerializeField] private int difficulty;
    [SerializeField] private int income;
    private int cost;

    private PlayerLogic player;

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Try();
        });

        cancelButton.onClick.AddListener(() =>
        {
            Close();
        });

        endScreenButton.onClick.AddListener(() =>
        {
            Close();
        });

        cost = difficulty * 10;

        playerCommand = new PlayerLogic.AlterBalanceCommand()
        {
            NextCommand = new PlayerLogic.AlterPassiveIncomeCommand()
            {
                Parameters = new PlayerLogic.SimpleIntegerParam() { integer = income }
            },
            Parameters = new PlayerLogic.SimpleIntegerParam() { integer = -cost }
        };
    }

    public override void Interact()
    {
        player = TurnManager.Instance.CurrentPlayer;
        playerCommand.TargetPlayer = player;
        playerCommand.NextCommand.TargetPlayer = player;

        costText.text = $"Cost: {cost}00$";
        difficultyText.text = $"Difficulty: {difficulty}";

        warning.SetActive(false);
        rolledNumberText.gameObject.SetActive(false);
        endScreen.SetActive(false);

        OnPlayerEnterTender?.Invoke();

        Show();
    }

    private async void Try()
    {
        if (player.Money < cost)
        {
            warning.SetActive(true);
            return;
        }

        bool isVictorious = await Roll();

        endScreen.SetActive(true);

        if (isVictorious)
        {
            endScreenText.text = "You won and now have factory here.";
            playerCommand.Execute();
        }
        else
        {
            endScreenText.text = "You loose and can't have factory here";
        }
    }

    private async Task<bool> Roll()
    {
        int rolledNumber = 0;
        rolledNumberText.gameObject.SetActive(true);
        for (int i = 0; i < 20; i++)
        {
            rolledNumber = UnityEngine.Random.Range(2, 13);
            rolledNumberText.text = rolledNumber.ToString();
            await Task.Delay(100);
        }

        await Task.Delay(1000);

        return rolledNumber > difficulty;
    }

    private void Close()
    {
        Hide();
        EndTurn();
    }
}
