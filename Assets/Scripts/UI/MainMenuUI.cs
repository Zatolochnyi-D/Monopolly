using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject newGameScreen;

    void Awake()
    {
        newGameButton.onClick.AddListener(() =>
        {
            newGameScreen.SetActive(true);
            gameObject.SetActive(false);
        });

        loadGameButton.onClick.AddListener(() =>
        {
            // to load screen
        });

        newGameButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
