using System.Collections.Generic;
using UnityEngine;

public class PawnSelectionManager : MonoBehaviour
{
    public static PawnSelectionManager Instance { get; private set; }

    [SerializeField] private Sprite[] pawnImages;

    private List<SimpleCircleGalleryUIC> galleries = new();
    private bool[] availablePawns;

    void Awake()
    {
        Instance = this;
        availablePawns = new bool[pawnImages.Length];
        for (int i = 0; i < availablePawns.Length; i++) availablePawns[i] = true;
    }

    void Start()
    {
        // for (int i = 0; i < galleries.Count; i++)
        // {
        //     galleries[i].SetUpComponent(pawnImages);
        //     galleries[i].SetSelectionIndex(i);
        // }

        // for (int i = 0; i < galleries.Count; i++)
        // {
        //     foreach (SimpleCircleGalleryUIC gallery in galleries)
        //     {
        //         if (gallery == galleries[i]) continue;
        //         gallery.Availability[i] = false;
        //     }
        // }

        // foreach (SimpleCircleGalleryUIC gallery in galleries)
        // {
        //     gallery.OnValueChanged += UpdateAvailability;
        // }
    }

    public void AddGallery(SimpleCircleGalleryUIC gallery)
    {
        gallery.SetUpComponent(pawnImages);
        // for (int i = 0; i < availablePawns.Length; i++)
        // {
        //     if (!availablePawns[i]) gallery.Availability[i] = false;
        // }
        // for (int i = 0; i < availablePawns.Length; i++)
        // {
        //     if (availablePawns[i])
        //     {
        //         gallery.SetSelectionIndex(i);
        //         availablePawns[i] = false;
        //         foreach (SimpleCircleGalleryUIC otherGallery in galleries)
        //         {
        //             otherGallery.Availability[i] = false;
        //         }
        //         break;
        //     }
        // }


        foreach (SimpleCircleGalleryUIC otherGallery in galleries)
        {
            gallery.Availability[otherGallery.CurrentIndex] = false;
        }
        for (int i = 0; i < gallery.Availability.Length; i++)
        {
            if (gallery.Availability[i])
            {
                gallery.SetSelectionIndex(i);
                break;
            }
        }
        foreach (SimpleCircleGalleryUIC otherGallery in galleries)
        {
            otherGallery.Availability[gallery.CurrentIndex] = false;
        }
        galleries.Add(gallery);
    }

    public void RemoveGallery(SimpleCircleGalleryUIC gallery)
    {
        if (!galleries.Contains(gallery)) return;
        foreach (SimpleCircleGalleryUIC otherGallery in galleries)
        {
            otherGallery.Availability[gallery.CurrentIndex] = true;
        }
        galleries.Remove(gallery);
    }

    public void UpdateAvailability(object sender, CirlceGalleryEventArgs args)
    {
        foreach (SimpleCircleGalleryUIC gallery in galleries)
        {
            if (gallery == (SimpleCircleGalleryUIC)sender) continue;
            gallery.Availability[args.previousIndex] = true;
            gallery.Availability[args.currentIndex] = false;
        }
    }
}
