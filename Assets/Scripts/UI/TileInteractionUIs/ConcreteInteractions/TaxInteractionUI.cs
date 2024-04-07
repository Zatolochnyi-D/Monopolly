using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaxInteractionUI : InteractionUI
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private float taxRate = -0.1f;

    void Awake()
    {
        confirmButton.onClick.AddListener(() => 
        {
            Close();
        });

        playerCommand = new PlayerLogic.MultiplyAddWithCapBalanceCommand();
    }

    public override void Interact(PlayerLogic player)
    {
        playerCommand.SetReceiver(player);

        if (player.Money >= 10)
        {
            playerCommand.Execute(new PlayerLogic.SimpleFloatParam() { floating = taxRate });
            descriptionText.text = "Tax Service have caught you! \n-10% of your current balance.";
        }
        else
        {
            descriptionText.text = "Tax Service have caught you! \nBut they let you go.";
        }
        
        Show();
    }

    private void Close()
    {
        Hide();
        EndTurn();
    }
}
