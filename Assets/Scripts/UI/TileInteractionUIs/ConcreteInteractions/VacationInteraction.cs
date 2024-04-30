using UnityEngine;
using UnityEngine.UI;

public class VacationInteraction : Interaction
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
