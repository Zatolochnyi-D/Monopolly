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

        playerCommand = new PlayerLogic.AddBalanceCommand();
    }

    public override void Interact(PlayerLogic player)
    {
        Show();
        playerCommand.SetReceiver(player);

        if (player.Image > 0)
        {
            int defaultBonusPerImage = 5;
            PlayerLogic.SimpleIntegerParam bonus = new() { integer = player.Image * defaultBonusPerImage };
            descriptionText.text = $"Your received bonus for a good job. \n+{bonus.integer}00$";
            playerCommand.Execute(bonus);
        }
        else
        {
            descriptionText.text = "Your reputations is too low. No bonus for you.";
        }
    }

    private void Confirm()
    {
        Hide();
        EndTurn();
    }
}
