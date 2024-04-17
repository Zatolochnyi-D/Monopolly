using UnityEngine;

public class PawnSelectionManager : MonoBehaviour
{
    [SerializeField] private SimpleCircleGalleryUIC[] galleries;
    [SerializeField] private Sprite[] pawnImages;

    void Start()
    {
        for (int i = 0; i < galleries.Length; i++)
        {
            galleries[i].SetUpComponent(pawnImages);
            galleries[i].SetSelectionIndex(i);
        }

        for (int i = 0; i < galleries.Length; i++)
        {
            foreach (SimpleCircleGalleryUIC gallery in galleries)
            {
                if (gallery == galleries[i]) continue;
                gallery.Availability[i] = false;
            }
        }

        foreach (SimpleCircleGalleryUIC gallery in galleries)
        {
            gallery.OnValueChanged += UpdateAvailability;
        }
    }

    public void UpdateAvailability(object sender, SimpleCircleGalleryUIC.CirlceGalleryEventArgs args)
    {
        foreach (SimpleCircleGalleryUIC gallery in galleries)
        {
            if (gallery == (SimpleCircleGalleryUIC)sender) continue;
            gallery.Availability[args.previousIndex] = true;
            gallery.Availability[args.currentIndex] = false;
        }
    }
}
