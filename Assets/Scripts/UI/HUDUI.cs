using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUDUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI whoseTurnText;
    [SerializeField] private TextMeshProUGUI rolledNumberDisplayText;

    void Start()
    {
        UpdateWhoseTurnText();

        TurnManager.Instance.TurnPassed += UpdateWhoseTurnText;

        // DiceManager.Instance.DiceRolled += UpdateRolledNumberDisplayText;
    }

    private void UpdateWhoseTurnText()
    {
        string playerName = TurnManager.Instance.CurrentPlayerData.PlayerName;
        string playerDisplayHexColor = $"#{TurnManager.Instance.CurrentPlayerData.DisplayColor.ToHexString()}";

        string textToShow = $"<{playerDisplayHexColor}>{playerName}</color>'s turn";

        whoseTurnText.text = textToShow;
    }

    private void UpdateRolledNumberDisplayText(int rolledNumber)
    {
        rolledNumberDisplayText.text = rolledNumber.ToString();
    }
}
