using UnityEngine;
using UnityEngine.UI;

public class NegativePRInteractionUI : InteractionUI
{
    [SerializeField] private Button confirmButton;

    private PlayerLogic.DecreaseImageMultiplyAdditively playerCommand = new();

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Accept();
        });
    }

    private void Accept()
    {
        currentPlayer = null;
        Hide();
        EndTurn();
    }

    public override void Iteract(PlayerLogic player)
    {
        currentPlayer = player;
        Show();
        playerCommand.coeffitient = 0.1f;
        playerCommand.Execute(currentPlayer);
    }
}
