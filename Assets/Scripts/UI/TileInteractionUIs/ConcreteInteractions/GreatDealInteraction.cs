using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GreatDealInteraction : Interaction
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
    [SerializeField] private TileLogic targetTile;
    [SerializeField] private bool resetImageOnSuccess = true;

    private NumberRoller roller;
    private PlayerLogic player;

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
        difficultyText.text = difficulty.ToString();
        roller = new(rolledNumberText, Enumerable.Range(2, 12).ToArray());

        playerCommand = new PlayerLogic.AlterBalanceCommand()
        {
            NextCommand = new PlayerLogic.AlterImageCommand()
            {
                NextCommand = new PlayerLogic.MovePlayerCommand()
            }
        };
    }

    public override void Interact()
    {
        player = TurnManager.Instance.CurrentPlayer;
        playerCommand.TargetPlayer = player;

        endScreen.SetActive(false);
        rolledNumberText.gameObject.SetActive(false);

        if (player.Money < 0)
        {
            endScreen.SetActive(true);
            endScreenText.text = "You can't leave small circle with the debt.";
        }

        if (2 > difficulty)
        {
            endScreen.SetActive(true);
            endScreenText.text = $"This deal is to easy. Your received {yield}00$ and pass to the big circle";

            SetParameters(yield, resetImageOnSuccess ? -player.Image : 0, targetTile);
            playerCommand.Execute();
        }

        Show();
    }

    private async void Play()
    {
        rolledNumberText.gameObject.SetActive(true);

        int rolledNumber = await roller.Roll();

        endScreen.SetActive(true);

        if (rolledNumber > difficulty)
        {
            SetParameters(yield, resetImageOnSuccess ? -player.Image : 0, targetTile);
            endScreenText.text = $"You handled this deal! \n+{yield}00$";
            endScreen.SetActive(true);
        }
        else
        {
            SetParameters(0, -3, null);
            endScreenText.text = $"You lost this deal. \n-3 image";
            endScreen.SetActive(true);
        }
        playerCommand.Execute();
    }

    private void SetParameters(int money, int image, TileLogic tile)
    {
        playerCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = money };
        playerCommand.NextCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = image };
        playerCommand.NextCommand.NextCommand.Parameters = new PlayerLogic.SimpleTileParam() { tile = tile };
    }
}