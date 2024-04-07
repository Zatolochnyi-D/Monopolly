using UnityEngine;
using UnityEngine.UI;

public class RecomendationsInteractionUI : InteractionUI
{
    [SerializeField] private Button confirmButton;

    private float imageMultiplier = 0.2f;

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Close();
        });

        playerCommand = new PlayerLogic.AddImagePercentFromHighestCommand();
    }

    public override void Interact(PlayerLogic player)
    {
        playerCommand.SetReceiver(player);
        playerCommand.Execute(new PlayerLogic.SimpleFloatParam() { floating = imageMultiplier });
        Show();
    }

    private void Close()
    {
        Hide();
        EndTurn();
    }
}
