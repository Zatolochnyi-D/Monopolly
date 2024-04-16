using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleCircleGalleryUIC : MonoBehaviour, IPointerClickHandler
{
    public event Action<int> OnValueChanged;

    [SerializeField] private Sprite[] images;

    private Image displayedImage;
    private int displayerImagePosition = 0;
    private int currentSelectionIndex = 0;
    private bool[] availability;

    void Awake()
    {
        displayedImage = transform.GetChild(displayerImagePosition).GetComponent<Image>();
        displayedImage.sprite = images[currentSelectionIndex];

        availability = new bool[images.Length];
        for (int i = 0; i < availability.Length; i++) availability[i] = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int nextIndex;
        do
        {
            nextIndex = (currentSelectionIndex + 1) % images.Length;
            if (nextIndex == currentSelectionIndex) return;
        }
        while (!availability[nextIndex]);

        SetSelectionIndex(nextIndex);
    }

    public void SetSelectionIndex(int index)
    {
        if (index < 0 || index > images.Length)
        {
            Debug.LogError("Index out of range.");
            return;
        }

        currentSelectionIndex = index;
        displayedImage.sprite = images[currentSelectionIndex];
        OnValueChanged?.Invoke(index);
    }
}
