using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusInteractionUI : InteractionUI
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button confirmButton;

    private PlayerLogic.AlterBalanceAdditively playerCommand = new();

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Confirm();
        });
    }

    public override void Iteract(PlayerLogic player)
    {
        Show();

        if (player.Image > 0)
        {
            int defaultBonusPerImage = 5;
            int bonus = player.Image * defaultBonusPerImage;
            descriptionText.text = $"Your received bonus for a good job. \n+{bonus}00$";
            playerCommand.balance = bonus;
            playerCommand.Execute(player);
        }
        else
        {
            descriptionText.text = "Your reputations is to low. No bonus for you.";
        }
    }

    private void Confirm()
    {
        Hide();
        EndTurn();
    }
}
