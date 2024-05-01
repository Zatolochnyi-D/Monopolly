using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class NumberRoller
{
    private TextMeshProUGUI displayText;
    private readonly int[] range;

    public TextMeshProUGUI DisplayText { get => displayText; set => displayText = value; }

    public NumberRoller(TextMeshProUGUI displayText, int[] range)
    {
        this.displayText = displayText;
        this.range = range;
    }

    public async Task<int> Roll()
    {
        int rolledNumber = 0;
        displayText.gameObject.SetActive(true);
        for (int i = 0; i < 20; i++)
        {
            rolledNumber = range[Random.Range(0, range.Length)];
            displayText.text = rolledNumber.ToString();
            await Task.Delay(100);
        }
        await Task.Delay(1000);

        return rolledNumber;
    }
}
