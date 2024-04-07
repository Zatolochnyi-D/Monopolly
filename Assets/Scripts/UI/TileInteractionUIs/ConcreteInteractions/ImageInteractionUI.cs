using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageInteractionUI : InteractionUI
{
    [SerializeField] private ImageEventsListSO events;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI warning;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI endScreenDescription;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI imageText;
    [SerializeField] private Button endScreenButton;

    private PlayerLogic player;

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            TryBuyImage();
        });

        cancelButton.onClick.AddListener(() =>
        {
            Close();
        });

        endScreenButton.onClick.AddListener(() =>
        {
            Close();
        });

        playerCommand = new PlayerLogic.ImageMoneyExchangeCommand();
    }

    public override void Interact(PlayerLogic player)
    {
        this.player = player;
        playerCommand.SetReceiver(player);

        warning.gameObject.SetActive(false);
        endScreen.SetActive(false);

        Show();
    }

    private void TryBuyImage()
    {
        if (player.Money <= 0)
        {
            warning.gameObject.SetActive(true);
            return;
        }

        int selectedEventID = UnityEngine.Random.Range(1, events.texts.Count);

        endScreenDescription.text = events.texts[selectedEventID];
        costText.text = $"Cost: {events.numbers[selectedEventID].y}";
        imageText.text = $"Image: {events.numbers[selectedEventID].x}";
        playerCommand.Execute(new PlayerLogic.DoubleIntegerParam() { first = events.numbers[selectedEventID].x, second = -events.numbers[selectedEventID].y });
        endScreen.SetActive(true);
    }

    private void Close()
    {
        Hide();
        EndTurn();
    }
}
