using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PawnPreviewUI : MonoBehaviour, IPointerClickHandler
{
    public event Action<int> OnOptionSelected;

    private ImageListUI imageList;
    private GameObject selectedVisual;
    private Image selectedVisualImage;

    private float delayBeforeOpen = 0.1f;
    private AsyncTimer delayBeforeOpenTimer;

    private bool canOpenList = true;

    void Awake()
    {
        delayBeforeOpenTimer = new(delayBeforeOpen);
        delayBeforeOpenTimer.OnTimeOut += SwitchToActive;

        imageList = transform.GetChild(0).GetComponent<ImageListUI>();

        selectedVisual = transform.GetChild(1).gameObject;
        selectedVisualImage = selectedVisual.GetComponent<Image>();
    }

    void Start()
    {
        imageList.OnListDisabled += StartDelayTimer;
        imageList.OnOptionSelected += (int option) => OnOptionSelected?.Invoke(option);
    }

    private void SwitchToActive()
    {
        canOpenList = true;
    }

    private void StartDelayTimer()
    {
        delayBeforeOpenTimer.Start();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");

        if (canOpenList)
        {
            imageList.Enable();
            canOpenList = false;
        }
    }

    public void FillImageList(Sprite[] options)
    {
        imageList.FillList(options);
    }

    public void SetPreview(Sprite sprite)
    {
        selectedVisualImage.sprite = sprite;
    }

    public void UpdateAvailability(bool[] availabilityMap)
    {
        imageList.UpdateAvailability(availabilityMap);
    }
}
