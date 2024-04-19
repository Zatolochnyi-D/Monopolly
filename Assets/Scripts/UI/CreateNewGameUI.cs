using UnityEngine;
using UnityEngine.UI;

public class CreateNewGameUI : MonoBehaviour
{
    [SerializeField] private Button startGameButton;

    void Awake()
    {
        startGameButton.onClick.AddListener(() =>
        {
            NewGameManager.Instance.CreateGame();
        });
    }
}
