using UnityEngine;
using UnityEngine.UI;

public class VacationInteractionUI : InteractionUI
{
    [SerializeField] private Button confirmationButton;

    void Awake()
    {
        confirmationButton.onClick.AddListener(() =>
        {
            DoNothing();
        });
    }

    public override void Interact()
    {
        Show();
    }

    private void DoNothing()
    {
        Hide();
        EndTurn();
    }
}
