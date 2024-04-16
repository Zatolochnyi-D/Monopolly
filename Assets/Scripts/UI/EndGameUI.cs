using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button toMainMenuButton;

    void Awake()
    {
        toMainMenuButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scenes.MainMenu);
        });

        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        GameInputManager.Instance.Disable();
        PlayerLogic winner = EndGameManager.Instance.Winner;
        text.text = $"<#{winner.DisplayColor.ToHexString()}>{winner.PlayerName}</color> is winner!";
    }
}
