using TMPro;
using UnityEngine;

public class PlayerStatsSheetUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameDisplay;
    [SerializeField] private TextMeshProUGUI numberDisplay;
    [SerializeField] private TextMeshProUGUI balanceDisplay;
    [SerializeField] private TextMeshProUGUI imageDisplay;

    public string Name
    {
        get => nameDisplay.text;
        set => nameDisplay.text = value;
    }
    public string Number
    {
        get => numberDisplay.text;
        set => numberDisplay.text = value;
    }
    public string Balance
    {
        get => balanceDisplay.text[0..^3];
        set => balanceDisplay.text = value + "00$"; // 1 money in code = 100 money displayed.
    }
    public string Image
    {
        get => imageDisplay.text;
        set => imageDisplay.text = value;
    }
}
