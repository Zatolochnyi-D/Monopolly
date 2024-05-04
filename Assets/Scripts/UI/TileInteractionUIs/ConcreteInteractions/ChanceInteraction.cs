using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChanceInteraction : Interaction
{
    [SerializeField] private ChanceEventsListSO events;
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI endScreenDescription;
    [SerializeField] private TextMeshProUGUI effectText;
    [SerializeField] private Button endScreenButton;

    private PlayerLogic player;

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            RollRandomEvent();
        });

        endScreenButton.onClick.AddListener(() =>
        {
            Close();
        });

        playerCommand = new PlayerLogic.AlterBalanceCommand();
    }

    public override void Interact()
    {
        player = TurnManager.Instance.CurrentPlayer;
        playerCommand.TargetPlayer = player;

        endScreen.SetActive(false);

        Show();
    }

    private void RollRandomEvent()
    {
        int selectedEventID = Random.Range(1, events.texts.Count);

        endScreenDescription.text = events.texts[selectedEventID];
        effectText.text = $"Effect: {events.effects[selectedEventID]}00$";
        SetParameters(events.effects[selectedEventID]);
        playerCommand.Execute();
        endScreen.SetActive(true);
    }

    private void SetParameters(int money)
    {
        playerCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = money};
    }
}
