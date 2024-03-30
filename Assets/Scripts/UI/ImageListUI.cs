using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageListUI : MonoBehaviour
{
    public event Action OnListDisabled;
    public event Action<int> OnOptionSelected;

    // image list components
    private RectTransform listBackground;
    private Transform container;
    private Transform template;

    // container grid layout info
    private int[] padding;
    private Vector2 cellSize;
    private Vector2 spacing;
    private int oneRowCapacity;

    void Awake()
    {
        listBackground = transform.GetChild(0).GetComponent<RectTransform>();
        container = transform.GetChild(1);
        template = container.GetChild(0);

        GridLayoutGroup containerGridLayout = container.GetComponent<GridLayoutGroup>();
        padding = new int[4];
        RectOffset containerGridPadding = containerGridLayout.padding;
        padding[0] = containerGridPadding.left;
        padding[1] = containerGridPadding.right;
        padding[2] = containerGridPadding.top;
        padding[3] = containerGridPadding.bottom;
        cellSize = containerGridLayout.cellSize;
        spacing = containerGridLayout.spacing;
        oneRowCapacity = containerGridLayout.constraintCount;

        template.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    
    void Update()
    {
        // TODO replace with new input system
        if (Input.GetMouseButtonDown(0))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(listBackground, Input.mousePosition))
            {
                Disable();
            }
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        OnListDisabled?.Invoke();
    }

    public void FillList(Sprite[] options)
    {
        Awake();

        foreach (Transform child in container)
        {
            if (child == template) continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < options.Length; i++)
        {
            Transform option = Instantiate(template, container);

            Image optionImage = option.GetComponent<Image>();
            optionImage.sprite = options[i];

            Button optionButton = option.GetComponent<Button>();
            int cached = i;
            optionButton.onClick.AddListener(() =>
            {
                OnOptionSelected?.Invoke(cached);
                Disable();
            });

            option.gameObject.SetActive(true);
        }

        int rowsAmount = (int)MathF.Ceiling((float)options.Length / oneRowCapacity);

        Vector2 newSize = new()
        {
            x = padding[0] + padding[1] + spacing.x * (oneRowCapacity - 1) + cellSize.x * oneRowCapacity,
            y = padding[2] + padding[3] + spacing.y * (rowsAmount - 1) + cellSize.y * rowsAmount
        };

        listBackground.sizeDelta = newSize;
    }

    public void UpdateAvailability(bool[] availabilityMap)
    {
        int i = 0;
        foreach (Transform child in container)
        {
            if (child == template) continue;
            Button button = child.GetComponent<Button>();
            button.interactable = availabilityMap[i];
            i++;
        }
    }
}
