using TMPro;
using UnityEngine;

public class PlayerFieldUI : MonoBehaviour
{
    [SerializeField] private SimpleCircleGalleryUIC gallery;
    [SerializeField] private TMP_InputField inputField;

    private int fieldIndex;

    public void InitField(int index)
    {
        fieldIndex = index;

        PawnSelectionManager.Instance.AddGallery(gallery);

        int nameIndex = NameSelectionManager.Instance.AddPlayer();
        inputField.text = NameSelectionManager.Instance.Names[nameIndex];
        inputField.onEndEdit.AddListener((value) => NameSelectionManager.Instance.ChangeName(fieldIndex, value));
    }

    public void DisposeField()
    {
        PawnSelectionManager.Instance.RemoveGallery(gallery);
        NameSelectionManager.Instance.RemovePlayer(fieldIndex);
    }

    public void DecreaseIndex()
    {
        fieldIndex--;
    }
}
