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

        TurnManager.Instance.OnTurnEnded += PrepareHUDForNewPlayer;

        DiceManager.Instance.OnRollDiceTriggered += HideDiceButton;

        DiceThrowingManager.Instance.OnDiceMovementStopped += ShowRolledNumber;
    }

    private void PrepareHUDForNewPlayer()
    {
        UpdateWhoseTurnText();
        ClearRolledNumberDisplayText();
        ShowDiceButton();
    }

    private void ShowRolledNumber(int rolledNumber)
    {
        rolledNumberDisplayText.text = rolledNumber.ToString();
    }

    private void ShowDiceButton()
    {
        rollDiceButton.gameObject.SetActive(true);
    }

    private void HideDiceButton()
    {
        rollDiceButton.gameObject.SetActive(false);
    }

    private void ClearRolledNumberDisplayText()
    {
        rolledNumberDisplayText.text = "";
    }

    private void UpdateWhoseTurnText()
    {
        string playerName = TurnManager.Instance.CurrentPlayer.PlayerName;
        string playerDisplayHexColor = $"#{TurnManager.Instance.CurrentPlayer.DisplayColor.ToHexString()}";

        string textToShow = $"<{playerDisplayHexColor}>{playerName}</color>'s turn";

        whoseTurnText.text = textToShow;
    }
}
