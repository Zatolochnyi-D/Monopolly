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
            string loadedSerializedGame = FileManager.Load();
            if (loadedSerializedGame == null) return;

            TurnManager.loadFromSnapshot = Saver.DeserializeGame(loadedSerializedGame);

            Loader.LoadScene(Loader.Scenes.Game);

            // to load screen
        });

        newGameButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
