using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusInteractionUI : InteractionUI
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button confirmButton;

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Confirm();
        });

        playerCommand = new PlayerLogic.AlterBalanceCommand();
    }

    public override void Interact()
    {
        PlayerLogic player = TurnManager.Instance.CurrentPlayer;
        playerCommand.TargetPlayer = player;

        if (player.Image > 0)
        {
            int defaultBonusPerImage = 5;
            PlayerLogic.SimpleIntegerParam bonus = new() { integer = player.Image * defaultBonusPerImage };
            descriptionText.text = $"Your received bonus for a good job. \n+{bonus.integer}00$";
            playerCommand.Parameters = bonus;
            playerCommand.Execute();
        }
        else
        {
            descriptionText.text = "Your reputations is too low. No bonus for you.";
        }

        Show();
    }

    private void Confirm()
    {
        Hide();
        EndTurn();
    }
}
