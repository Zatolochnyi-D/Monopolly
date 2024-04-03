using UnityEngine;
using UnityEngine.UI;

public class NegativePRInteractionUI : InteractionUI
{
    [SerializeField] private Button confirmButton;

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

    public override void Iteract(PlayerLogic player)
    {
        Show();
        playerCommand.SetReceiver(player);
        playerCommand.Execute(new PlayerLogic.SimpleFloatParam() { floating = -0.1f });
    }
}
