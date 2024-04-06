using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDealInteractionUI : InteractionUI
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI yieldText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI rolledNumberText;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI endScreenText;
    [SerializeField] private Button endScreenButton;

    [SerializeField] private int yield;
    [SerializeField] private int difficulty;

    private int currentDifficulty;

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Play();
        });

        cancelButton.onClick.AddListener(() =>
        {
            Close();
        });

        endScreenButton.onClick.AddListener(() =>
        {
            Close();
        });

        yieldText.text = $"{yield}00$";

        playerCommand = new PlayerLogic.ImageMoneyExchangeCommand();
    }

    public override void Iteract(PlayerLogic player)
    {
        playerCommand.SetReceiver(player);

        endScreen.SetActive(false);
        rolledNumberText.gameObject.SetActive(false);

        if (player.Image >= difficulty)
        {
            endScreen.SetActive(true);
            endScreenText.text = $"Your reputation successfully handled this deal. Your received {yield}00$.";
            playerCommand.Execute(new PlayerLogic.DoubleIntegerParam() { second = yield });
        }

        if (difficulty - player.Image > 12)
        {
            endScreen.SetActive(true);
            endScreenText.text = $"Your reputation is to low for this deal.";
        }

        currentDifficulty = difficulty - player.Image;
        difficultyText.text = currentDifficulty.ToString();

        Show();
    }

    private async void Play()
    {
        rolledNumberText.gameObject.SetActive(true);

        bool isVictorious = await Roll();

        endScreen.SetActive(true);

        PlayerLogic.DoubleIntegerParam intToAdd = new();
        if (isVictorious)
        {
            intToAdd.second = yield;
            endScreenText.text = $"You handled this deal! \n+{intToAdd.second}00$";
            endScreen.SetActive(true);
        }
        else
        {
            intToAdd.first = -1;
            endScreenText.text = $"You lost this deal. \n-1 image";
            endScreen.SetActive(true);
        }
        playerCommand.Execute(intToAdd);
    }

    private async Task<bool> Roll()
    {
        int rolledNumber = 0;
        rolledNumberText.gameObject.SetActive(true);
        for (int i = 0; i < 20; i++)
        {
            rolledNumber = Random.Range(1, 13);
            rolledNumberText.text = rolledNumber.ToString();
            await Task.Delay(100);
        }

        await Task.Delay(1000);

        return rolledNumber >= currentDifficulty;
    }

    private void Close()
    {
        Hide();
        EndTurn();
    }
}
