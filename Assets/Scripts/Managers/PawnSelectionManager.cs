using System;
using System.Collections.Generic;
using UnityEngine;

public class PawnSelectionManager : MonoBehaviour
{
    public static PawnSelectionManager Instance { get; private set; }

    [SerializeField] private Sprite[] pawnImages;
    [SerializeField] private Color[] pawnCorrespondingColors;

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

    public PawnVisualsSO[] GetVisuals()
    {
        PawnVisualsSO[] result = new PawnVisualsSO[galleries.Count];
        List<Sprite> availableSprites = new(pawnImages[0..^1]);
        List<Color> availableColors = new(pawnCorrespondingColors);
        List<int> used = new();

        for (int i = 0; i < galleries.Count; i++)
        {
            int galleryIndex = galleries[i].CurrentIndex;
            if (galleryIndex == pawnImages.Length - 1) continue;
            result[i] = new() { visual = availableSprites[galleryIndex], displayColor = availableColors[galleryIndex] };
            used.Add(galleryIndex);
        }
        foreach (int i in used)
        {
            availableSprites.RemoveAt(i);
            availableColors.RemoveAt(i);
        }
        for (int i = 0; i < galleries.Count; i++)
        {
            if (galleries[i].CurrentIndex != pawnImages.Length - 1) continue;
            int randomIndex = UnityEngine.Random.Range(0, availableSprites.Count);
            result[i] = new() { visual = availableSprites[randomIndex], displayColor = availableColors[randomIndex] };
            availableSprites.RemoveAt(randomIndex);
            availableColors.RemoveAt(randomIndex);
        }

        return result;
    }

    public int GetIdByVisual(Sprite sprite)
    {
        return Array.IndexOf(pawnImages, sprite);
    }

    public PawnVisualsSO GetVisualById(int id)
    {
        return new() { visual = pawnImages[id], displayColor = pawnCorrespondingColors[id] };
    }
}
