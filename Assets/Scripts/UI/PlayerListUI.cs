using TMPro;
using Unity.VisualScripting;
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
            NewGameOptionsManager.Instance.AddPlayer();
        });
        removeButtonDivTemplate.gameObject.SetActive(false);

        NewGameOptionsManager.Instance.OnPlayersUpdated += UpdatePlayerList;
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

        for (int i = 0; i < NewGameOptionsManager.Instance.PlayerDatas.Count; i++)
        {
            Transform newPlayer = Instantiate(playerFieldTemplate, playerFieldsContainer);
            newPlayer.GetChild(0).GetComponent<TextMeshProUGUI>().text = NewGameOptionsManager.Instance.PlayerDatas[i].name;
            newPlayer.gameObject.SetActive(true);
        }
        buttonDiv.transform.SetAsLastSibling();

        if (!NewGameOptionsManager.Instance.IsMinPlayerLimitReached)
        {
            for (int i = 0; i < NewGameOptionsManager.Instance.PlayerDatas.Count; i++)
            {
                Transform newRemoveButtonDiv = Instantiate(removeButtonDivTemplate, removeButtonsContainer);
                newRemoveButtonDiv.gameObject.SetActive(true);
                Button newRemoveButton = newRemoveButtonDiv.GetChild(0).GetComponent<Button>();

                int cached = i;
                newRemoveButton.onClick.AddListener(() =>
                {
                    NewGameOptionsManager.Instance.RemovePlayer(cached);
                });
            }
        }

        if (NewGameOptionsManager.Instance.IsMaxPlayerLimitReached)
        {
            buttonDiv.gameObject.SetActive(false);
        }
        else
        {
            buttonDiv.gameObject.SetActive(true);
        }
    }
}
