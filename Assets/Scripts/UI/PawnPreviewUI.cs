using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PawnPreviewUI : MonoBehaviour, IPointerClickHandler
{
    private ImageListUI imageList;

    private float delayBeforeOpen = 0.1f;
    private AsyncTimer delayBeforeOpenTimer;

    private bool canOpenList = true;

    void Awake()
    {
        delayBeforeOpenTimer = new(delayBeforeOpen);
        delayBeforeOpenTimer.OnTimeOut += SwitchToActive;
    }

    void Start()
    {
        imageList = transform.GetChild(0).GetComponent<ImageListUI>();
        imageList.OnListDisabled += StartDelayTimer;
        imageList.OnOptionSelected += SaveSelectedOption;
    }

    private void SwitchToActive()
    {
        canOpenList = true;
    }

    private void StartDelayTimer()
    {
        delayBeforeOpenTimer.Start();
    }

    private void SaveSelectedOption(int index)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canOpenList)
        {
            imageList.Enable();
            canOpenList = false;
        }
    }
}
