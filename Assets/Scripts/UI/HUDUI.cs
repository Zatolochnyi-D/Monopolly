using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI whoseTurnText;
    [SerializeField] private TextMeshProUGUI rolledNumberDisplayText;
    [SerializeField] private Button rollDiceButton;

    void Start()
    {
        UpdateWhoseTurnText();
        ClearRolledNumberDisplayText();

        TurnManager.Instance.TurnPassed += UpdateWhoseTurnText;

        DiceManager.Instance.OnRollDiceTriggered += HideButton;

        DiceThrowingManager.Instance.OnDiceMovementStopped += ShowRolledNumber;
    }

    private void ShowRolledNumber(int rolledNumber)
    {
        rolledNumberDisplayText.text = rolledNumber.ToString();
    }

    private void HideButton()
    {
        rollDiceButton.gameObject.SetActive(false);
    }

    private void ClearRolledNumberDisplayText()
    {
        rolledNumberDisplayText.text = "";
    }

    private void UpdateWhoseTurnText()
    {
        string playerName = TurnManager.Instance.CurrentPlayerData.PlayerName;
        string playerDisplayHexColor = $"#{TurnManager.Instance.CurrentPlayerData.DisplayColor.ToHexString()}";

        string textToShow = $"<{playerDisplayHexColor}>{playerName}</color>'s turn";

        whoseTurnText.text = textToShow;
    }
}
