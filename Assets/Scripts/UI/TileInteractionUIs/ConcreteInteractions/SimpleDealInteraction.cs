using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDealInteraction : Interaction
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

    protected void Awake()
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

        playerCommand = new PlayerLogic.AlterBalanceCommand()
        {
            NextCommand = new PlayerLogic.AlterImageCommand()
        };
    }

    public override void Interact()
    {
        PlayerLogic player = TurnManager.Instance.CurrentPlayer;
        playerCommand.TargetPlayer = player;

        endScreen.SetActive(false);
        rolledNumberText.gameObject.SetActive(false);
        currentDifficulty = difficulty;

        if (player.Image - 2 >= difficulty)
        {
            endScreen.SetActive(true);
            endScreenText.text = $"Your reputation successfully handled this deal. Your received {yield}00$.";
            SetParameters(yield, 0);
            playerCommand.Execute();
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

        if (isVictorious)
        {
            SetParameters(yield, 0);
            endScreenText.text = $"You handled this deal! \n+{yield}00$";
            endScreen.SetActive(true);
        }
        else
        {
            SetParameters(0, -1);
            endScreenText.text = $"You lost this deal. \n-1 image";
            endScreen.SetActive(true);
        }
        playerCommand.Execute();
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

        return rolledNumber >= currentDifficulty;
    }

    private void Close()
    {
        Hide();
        EndTurn();
    }

    private void SetParameters(int money, int image)
    {
        playerCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = money };
        playerCommand.NextCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = image };
    }
}
