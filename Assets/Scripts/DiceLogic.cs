using TMPro;
using UnityEngine;

public class DiceLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        DiceManager.Instance.DiceRolled += (rolledNumber) =>
        {
            text.text = "Roll the dice: " + rolledNumber;
        };
    }
}
