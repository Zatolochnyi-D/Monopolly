using UnityEngine;
using UnityEngine.UI;

public class VacationInteraction : Interaction
{
    [SerializeField] private Button confirmationButton;

    void Awake()
    {
        confirmationButton.onClick.AddListener(() =>
        {
            Close();
        });
    }

    public override void Interact()
    {
        Show();
    }
}
