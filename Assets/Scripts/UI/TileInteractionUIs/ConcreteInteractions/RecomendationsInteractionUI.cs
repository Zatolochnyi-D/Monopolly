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

        playerCommand = new PlayerLogic.AlterImageCommand();
    }

    public override void Interact()
    {
        playerCommand.TargetPlayer = TurnManager.Instance.CurrentPlayer;

        int highestImage = TurnManager.Instance.HighestImageExcludeCurrentPlayer;
        int imageToAdd = Mathf.CeilToInt(Mathf.Abs(highestImage) * imageMultiplier);

        playerCommand.Parameters = new PlayerLogic.SimpleIntegerParam() { integer = imageToAdd };
        playerCommand.Execute();
        Show();
    }

    private void Close()
    {
        Hide();
        EndTurn();
    }
}
