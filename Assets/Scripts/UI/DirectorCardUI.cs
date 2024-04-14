using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DirectorCardUI : MonoBehaviour, IPointerClickHandler
{
    public event Action<string> OnClick;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image companyImage;
    [SerializeField] private TextMeshProUGUI powerText;

    public string Text => nameText.text;

    public void SetInfo(string name, Sprite image, int power)
    {
        nameText.text = name;
        companyImage.sprite = image;
        powerText.text = $"Power: {power}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(nameText.text);
    }
}
