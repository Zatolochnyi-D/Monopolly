using UnityEngine;
using UnityEngine.UI;

public class NegativePRInteractionUI : InteractionUI
{
    [SerializeField] private Button confirmButton;

    private float imageMultiplier = -0.1f;

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Accept();
        });

        playerCommand = new PlayerLogic.AlterImageCommand();
    }

    private void Accept()
    {
        Hide();
        EndTurn();
    }

    public override void Interact()
    {
        PlayerLogic player = TurnManager.Instance.CurrentPlayer;
        playerCommand.TargetPlayer = player;

        int imageToAdd;
        if (player.Image > 0)
        {
            imageToAdd = -Mathf.CeilToInt(player.Image * imageMultiplier);
        }
        else
        {
            imageToAdd = -1;
        }

        playerCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = imageToAdd };
        playerCommand.Execute();
        Show();
    }
}
