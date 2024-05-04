using UnityEngine;
using UnityEngine.UI;

public class PlayerListUI : MonoBehaviour
{
    [SerializeField] private Transform playerFieldsContainer;
    [SerializeField] private Transform playerFieldTemplate;
    [SerializeField] private Transform buttonDiv;
    [SerializeField] private Transform removeButtonsContainer;
    [SerializeField] private Transform removeButtonDivTemplate;

    private Button addPlayerButton;

    void Start()
    {
        addPlayerButton = buttonDiv.GetChild(0).GetComponent<Button>();

        playerFieldTemplate.gameObject.SetActive(false);
        addPlayerButton.onClick.AddListener(() =>
        {
            NewGameManager.Instance.AddPlayer();
        });
        removeButtonDivTemplate.gameObject.SetActive(false);
        removeButtonsContainer.gameObject.SetActive(false);

        NewGameManager.Instance.OnPlayerAdded += AddPlayer;
        NewGameManager.Instance.OnPlayerRemoved += DeletePlayer;
        for (int i = 0; i < NewGameManager.Instance.PlayerList.Count; i++) AddPlayer();
    }

    private void AddPlayer()
    {
        Transform newPlayer = Instantiate(playerFieldTemplate, playerFieldsContainer);
        newPlayer.gameObject.SetActive(true);
        var newPlayerScript = newPlayer.GetComponent<PlayerFieldUI>();
        int cached = playerFieldsContainer.childCount;
        newPlayerScript.InitField(cached - 3);
        buttonDiv.transform.SetAsLastSibling();

        Transform newRemoveButtonDiv = Instantiate(removeButtonDivTemplate, removeButtonsContainer);
        newRemoveButtonDiv.gameObject.SetActive(true);
        Button newRemoveButton = newRemoveButtonDiv.GetChild(0).GetComponent<Button>();
        newRemoveButton.onClick.AddListener(() => NewGameManager.Instance.RemovePlayer(cached - 3));

        if (!NewGameManager.Instance.IsMinPlayersReached)
        {
            removeButtonsContainer.gameObject.SetActive(true);
        }
        if (NewGameManager.Instance.IsMaxPlayersReached)
        {
            buttonDiv.gameObject.SetActive(false);
        }
    }

    private void DeletePlayer(int index)
    {
        Transform playerTemplate = playerFieldsContainer.GetChild(index + 1);
        playerTemplate.GetComponent<PlayerFieldUI>().DisposeField();
        for (int i = index + 2; i < playerFieldsContainer.childCount - 1; i++)
        {
            playerFieldsContainer.GetChild(i).GetComponent<PlayerFieldUI>().DecreaseIndex();
        }
        Destroy(playerTemplate.gameObject);
        buttonDiv.gameObject.SetActive(true);

        Transform removeButtonDiv = removeButtonsContainer.GetChild(index + 1);
        for (int i = index + 2; i < removeButtonsContainer.childCount; i++)
        {
            Button nextRemoveButton = removeButtonsContainer.GetChild(i).GetChild(0).GetComponent<Button>();
            nextRemoveButton.onClick.RemoveAllListeners();
            int cached = i;
            nextRemoveButton.onClick.AddListener(() => NewGameManager.Instance.RemovePlayer(cached - 2));
        }
        Destroy(removeButtonDiv.gameObject);

        if (NewGameManager.Instance.IsMinPlayersReached)
        {
            removeButtonsContainer.gameObject.SetActive(false);
        }
    }
}
