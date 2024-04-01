using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private PlayerStatsSheetUI statsSheetScript;
    [SerializeField] private Button hideButton;

    void Awake()
    {
        hideButton.onClick.AddListener(() =>
        {
            Hide();
        });

        gameObject.SetActive(false);
    }

    void Start()
    {
        UpdateStatsSheet();
        TurnManager.Instance.OnTurnEnded += UpdateStatsSheet;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateStatsSheet()
    {
        PlayerLogic player = TurnManager.Instance.CurrentPlayer;

        statsSheetScript.Name = player.PlayerName;
        statsSheetScript.Number = player.Number.ToString();
        statsSheetScript.Balance = player.Money.ToString();
        statsSheetScript.Image = player.Image.ToString();
    }
}
