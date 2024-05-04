using UnityEngine;
using UnityEngine.UI;

public class CreateNewGameUI : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button backToMenuButton;

    [SerializeField] private GameObject previousPage;

    void Awake()
    {
        startGameButton.onClick.AddListener(() =>
        {
            NewGameManager.Instance.CreateGame();
        });

        backToMenuButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            previousPage.SetActive(true);
        });
    }

    void Start()
    {
        gameObject.SetActive(false);
    }
}
