using System;
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
    }

    void Start()
    {
        UpdateStatsSheet();
        TurnManager.Instance.OnTurnEnded += UpdateStatsSheet;

        GameInputManager.Instance.OnOpenCloseStatsPreformed += SwitchStatsUIState;
        gameObject.SetActive(false);
    }

    private void SwitchStatsUIState()
    {
        if (gameObject.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
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
