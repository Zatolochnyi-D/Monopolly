using UnityEngine;
using UnityEngine.UI;

public class NegativePRInteraction : Interaction
{
    [SerializeField] private Button confirmButton;

    private float imageMultiplier = -0.1f;

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Close();
        });

        playerCommand = new PlayerLogic.AlterImageCommand();
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
