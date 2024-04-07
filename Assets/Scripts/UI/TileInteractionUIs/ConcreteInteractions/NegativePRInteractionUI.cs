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

        playerCommand = new PlayerLogic.MultiplyAddWithCapImageCommand();
    }

    private void Accept()
    {
        Hide();
        EndTurn();
    }

    public override void Interact(PlayerLogic player)
    {
        Show();
        playerCommand.SetReceiver(player);
        playerCommand.Execute(new PlayerLogic.SimpleFloatParam() { floating = imageMultiplier });
    }
}
