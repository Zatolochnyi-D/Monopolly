using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GreatDealInteractionUI : InteractionUI
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

        playerCommand = new PlayerLogic.MovePlayerAndChangeStatsCommand();
    }

    public override void Interact(PlayerLogic player)
    {
        playerCommand.SetReceiver(player);
        this.player = player;

        endScreen.SetActive(false);
        rolledNumberText.gameObject.SetActive(false);

        if (2 > difficulty)
        {
            endScreen.SetActive(true);
            endScreenText.text = $"This deal is to easy. Your received {yield}00$ and pass to the big circle";

            var param = new PlayerLogic.TileIntegersParam();
            param.tile.tile = targetTile;
            param.integers.second = yield;
            if (resetImageOnSuccess) param.integers.first = -player.Image;
            playerCommand.Execute(param);
        }

        Show();
    }

    private async void Play()
    {
        rolledNumberText.gameObject.SetActive(true);

        bool isVictorious = await Roll();

        endScreen.SetActive(true);

        PlayerLogic.TileIntegersParam param = new();
        if (isVictorious)
        {
            param.integers.second = yield;
            param.tile.tile = targetTile;
            endScreenText.text = $"You handled this deal! \n+{yield}00$";
            if (resetImageOnSuccess) param.integers.first = -player.Image;
            endScreen.SetActive(true);
        }
        else
        {
            param.integers.first = -3;
            endScreenText.text = $"You lost this deal. \n-3 image";
            endScreen.SetActive(true);
        }
        playerCommand.Execute(param);
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

        return rolledNumber > difficulty;
    }

    private void Close()
    {
        Hide();
        EndTurn();
    }
}