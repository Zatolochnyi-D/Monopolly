using System;
using System.Collections.Generic;
using System.Linq;
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
        List<int> unusedIndexes = Enumerable.Range(0, result.Length).ToList();

        for (int i = 0; i < result.Length; i++)
        {
            int index = Array.IndexOf(pawnImages, galleries[i].CurrentImage);
            if (index == pawnImages.Length - 1)
            {
                continue;
            }
            unusedIndexes.Remove(index);
            result[i] = new() { visual = pawnImages[index], displayColor = pawnCorrespondingColors[index] };
        }
        for (int i = 0; i < result.Length; i++)
        {
            if (result[i] == null)
            {
                int index = unusedIndexes[UnityEngine.Random.Range(0, unusedIndexes.Count - 1)];
                unusedIndexes.Remove(index);
                result[i] = new() { visual = pawnImages[index], displayColor = pawnCorrespondingColors[index] };
            }
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
