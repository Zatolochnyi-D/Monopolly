using UnityEngine;

public class PawnSelectionManager : MonoBehaviour
{
    public static PawnSelectionManager Instance { get; private set; }

    [SerializeField] private SimpleCircleGalleryUIC[] galleries;
    [SerializeField] private Sprite[] pawnImages;

    void Awake()
    {
        Instance = this;
    }

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
