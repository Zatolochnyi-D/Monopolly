using UnityEngine;

public class PlayerFieldUI : MonoBehaviour
{
    [SerializeField] private SimpleCircleGalleryUIC gallery;

    public void CreateGallery()
    {
        PawnSelectionManager.Instance.AddGallery(gallery);
    }

    public void DeleteGallery()
    {
        PawnSelectionManager.Instance.RemoveGallery(gallery);
    }
}
