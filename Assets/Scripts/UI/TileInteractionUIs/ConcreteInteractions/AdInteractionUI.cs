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

        playerCommand = new PlayerLogic.ImageMoneyExchangeCommand();
    }

    private void TryConfirm()
    {
        int cost = imageCostPairs[options.value].cost;
        if (currentPlayer.Money >= cost)
        {
            int image = imageCostPairs[options.value].image;
            playerCommand.Execute(new PlayerLogic.DoubleIntegerParam() { first = image, second = -cost });
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

    public override void Iteract(PlayerLogic player)
    {
        playerCommand.SetReceiver(player);
        currentPlayer = player;
        warningText.gameObject.SetActive(false);
        options.value = 2;
        Show();
    }
}
