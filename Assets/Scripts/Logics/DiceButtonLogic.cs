using TMPro;
using UnityEngine;

public class DiceButtonLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        DiceManager.Instance.DiceRolled += OnDiceRolled;
    }

    private void OnDiceRolled(int rolledNumber)
    {
        text.text = "Roll the dice: " + rolledNumber;
    }
}
