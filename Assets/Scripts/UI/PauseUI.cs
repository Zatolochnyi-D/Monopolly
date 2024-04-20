using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button saveAndQuitButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private PopUpUIC savePopup;
    [SerializeField] private PopUpUIC saveAndQuitPopup;
    [SerializeField] private PopUpUIC quitPopup;

    void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            Unpause();
        });
        saveButton.onClick.AddListener(() =>
        {
            savePopup.gameObject.SetActive(true);
        });
        saveAndQuitButton.onClick.AddListener(() =>
        {
            saveAndQuitPopup.gameObject.SetActive(true);
        });
        quitButton.onClick.AddListener(() =>
        {
            quitPopup.gameObject.SetActive(true);
        });

        savePopup.actionOnConfirm = () =>
        {
            FileManager.Save(Saver.SerializeGame(TurnManager.Instance.CreateSnapshot()));
            savePopup.gameObject.SetActive(false);
        };
        saveAndQuitPopup.actionOnConfirm = () =>
        {
            FileManager.Save(Saver.SerializeGame(TurnManager.Instance.CreateSnapshot()));
            Time.timeScale = 1.0f;
            Loader.LoadScene(Loader.Scenes.MainMenu);
        };
        quitPopup.actionOnConfirm = () =>
        {
            Time.timeScale = 1.0f;
            Loader.LoadScene(Loader.Scenes.MainMenu);
        };
    }

    void Start()
    {
        GameInputManager.Instance.OnPausePerformed += Pause;
        GameInputManager.Instance.OnUnpausePerformed += Unpause;

        TurnManager.Instance.OnTurnStarted += _ => DisableSaving();
        TurnManager.Instance.OnNewTurn += EnableSaving;

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
        savePopup.gameObject.SetActive(false);
        saveAndQuitPopup.gameObject.SetActive(false);
        quitPopup.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void DisableSaving()
    {
        saveAndQuitButton.enabled = false;
        saveButton.enabled = false;
    }
    
    private void EnableSaving()
    {
        saveAndQuitButton.enabled = true;
        saveButton.enabled = true;
    }
}
