using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdInteractionUI : InteractionUI
{
    [SerializeField] private TMP_Dropdown options;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI warningText;

    private PlayerLogic currentPlayer;
    private (int image, int cost)[] imageCostPairs = new[]
    {
        (2, 5),
        (3, 10),
        (4, 15)
    };

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            TryConfirm();
        });

        cancelButton.onClick.AddListener(() =>
        {
            Close();
        });

        options.onValueChanged.AddListener((int V) =>
        {
            warningText.gameObject.SetActive(false);
        });

        playerCommand = new PlayerLogic.AlterBalanceCommand()
        {
            NextCommand = new PlayerLogic.AlterImageCommand()
        };
    }

    private void TryConfirm()
    {
        int cost = imageCostPairs[options.value].cost;
        if (currentPlayer.Money >= cost)
        {
            int image = imageCostPairs[options.value].image;
            playerCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = -cost };
            playerCommand.NextCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = image };
            playerCommand.Execute();
            Close();
        }
        else
        {
            warningText.gameObject.SetActive(true);
        }
    }

    private void Close()
    {
        Hide();
        EndTurn();
    }

    public override void Interact(PlayerLogic player)
    {
        playerCommand.TargetPlayer = player;
        currentPlayer = player;
        warningText.gameObject.SetActive(false);
        options.value = 2;
        Show();
    }
}
