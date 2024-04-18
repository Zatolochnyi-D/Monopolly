using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleCircleGalleryUIC : MonoBehaviour, IPointerClickHandler
{
    public event EventHandler<CirlceGalleryEventArgs> OnValueChanged;

    private Sprite[] images;
    private Image displayedImage;
    private int displayerImagePosition = 0;
    private int currentSelectionIndex = 0;
    private bool[] availability;

    public bool[] Availability => availability;
    public int CurrentIndex => currentSelectionIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        int nextIndex = currentSelectionIndex;
        do
        {
            nextIndex = (nextIndex + 1) % images.Length;
            if (nextIndex == currentSelectionIndex) return;
        }
        while (!availability[nextIndex]);

        SetSelectionIndex(nextIndex);
    }

    public void SetUpComponent(Sprite[] images)
    {
        displayedImage = transform.GetChild(displayerImagePosition).GetComponent<Image>();
        this.images = images;
        availability = new bool[images.Length];
        for (int i = 0; i < availability.Length; i++) availability[i] = true;
    }

    public void SetSelectionIndex(int index)
    {
        if (index < 0 || index > images.Length)
        {
            Debug.LogError("Index out of range.");
            return;
        }

        int previousIndex = currentSelectionIndex;
        currentSelectionIndex = index;
        displayedImage.sprite = images[currentSelectionIndex];
        OnValueChanged?.Invoke(this, new CirlceGalleryEventArgs() { previousIndex = previousIndex, currentIndex = currentSelectionIndex});
    }
}


public class CirlceGalleryEventArgs : EventArgs
{
    public int previousIndex;
    public int currentIndex;
}