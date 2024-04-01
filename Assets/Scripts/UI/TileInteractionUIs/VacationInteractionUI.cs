using UnityEngine;
using UnityEngine.UI;

public class VacationInteractionUI : InteractionUI
{
    [SerializeField] private Button confirmationButton;

    private PlayerLogic currentPlayer;

    void Awake()
    {
        confirmationButton.onClick.AddListener(() =>
        {
            DoNothing();
        });
    }

    public override void Iteract(PlayerLogic player)
    {
        currentPlayer = player;
        Show();
    }

    private void DoNothing()
    {
        Hide();
        currentPlayer = null;
    }
}
