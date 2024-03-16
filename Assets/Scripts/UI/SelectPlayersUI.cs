using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayersUI : MonoBehaviour
{
    [SerializeField] private Button confirmationButton;
    [SerializeField] private TMP_Dropdown amountSelectionDropdown;

    void Awake()
    {
        confirmationButton.onClick.AddListener(() =>
        {
            // amountSelectionDropdown.value;

            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
