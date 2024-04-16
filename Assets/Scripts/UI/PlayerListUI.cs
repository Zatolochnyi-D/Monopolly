using TMPro;
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
            NewGameManager.Instance.PlayerList.AddPlayer();
        });
        removeButtonDivTemplate.gameObject.SetActive(false);

        NewGameManager.Instance.PlayerList.OnPlayersUpdated += UpdatePlayerList;
        UpdatePlayerList();
    }

    private void UpdatePlayerList()
    {
        foreach (Transform child in playerFieldsContainer)
        {
            if (child == playerFieldTemplate) continue;
            if (child == buttonDiv) continue;
            Destroy(child.gameObject);
        }

        foreach (Transform child in removeButtonsContainer)
        {
            if (child == removeButtonDivTemplate) continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < NewGameManager.Instance.PlayerList.Players.Count; i++)
        {
            Transform newPlayer = Instantiate(playerFieldTemplate, playerFieldsContainer);
            newPlayer.gameObject.SetActive(true);
        }
        buttonDiv.transform.SetAsLastSibling();

        if (!NewGameManager.Instance.PlayerList.IsMinPlayersReached)
        {
            for (int i = 0; i < NewGameManager.Instance.PlayerList.Players.Count; i++)
            {
                Transform newRemoveButtonDiv = Instantiate(removeButtonDivTemplate, removeButtonsContainer);
                newRemoveButtonDiv.gameObject.SetActive(true);
                Button newRemoveButton = newRemoveButtonDiv.GetChild(0).GetComponent<Button>();

                int cached = i;
                newRemoveButton.onClick.AddListener(() =>
                {
                    NewGameManager.Instance.PlayerList.RemovePlayer(cached);
                });
            }
        }

        if (NewGameManager.Instance.PlayerList.IsMaxPlayersReached)
        {
            buttonDiv.gameObject.SetActive(false);
        }
        else
        {
            buttonDiv.gameObject.SetActive(true);
        }
    }
}
