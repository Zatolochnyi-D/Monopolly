using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button quitButton;

    void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            Unpause();
        });

        saveButton.onClick.AddListener(() =>
        {
            FileManager.Save(Saver.SerializeGame(TurnManager.Instance.CreateSnapshot()));
            Time.timeScale = 1.0f;
            Loader.LoadScene(Loader.Scenes.MainMenu);
        });

        quitButton.onClick.AddListener(() =>
        {
            // ask for quit confirmation. Warn about data loss.

            // move this to quit confirmation
            Time.timeScale = 1.0f;
            Loader.LoadScene(Loader.Scenes.MainMenu);
        });
    }

    void Start()
    {
        GameInputManager.Instance.OnPausePerformed += Pause;
        GameInputManager.Instance.OnUnpausePerformed += Unpause;

        gameObject.SetActive(false);
    }

    private void Pause()
    {
        gameObject.SetActive(true);
        GameInputManager.Instance.ToPause();
        Time.timeScale = 0.0f;
    }
    
    private void Unpause()
    {
        gameObject.SetActive(false);
        GameInputManager.Instance.FromPause();
        Time.timeScale = 1.0f;
    }
}
