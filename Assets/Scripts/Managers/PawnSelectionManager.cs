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

    public void AddGallery(SimpleCircleGalleryUIC gallery)
    {
        gallery.SetUpComponent(pawnImages);
        foreach (SimpleCircleGalleryUIC otherGallery in galleries)
        {
            gallery.Availability[otherGallery.CurrentIndex] = false;
        }
        gallery.Availability[^1] = true;
        gallery.SetSelectionIndex(gallery.Availability.Length - 1);
        gallery.OnValueChanged += UpdateAvailability;
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
            gallery.Availability[args.previousIndex] = true;
            if (args.currentIndex != gallery.Availability.Length - 1) gallery.Availability[args.currentIndex] = false;
        }
    }
}
