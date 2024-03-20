using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStatsLogic : MonoBehaviour
{
    public enum PlayerColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }

    [SerializeField] private string playerName;
    [SerializeField] private PlayerColor selectedColor;
    [SerializeField] private int number;
    
    private Color displayColor;
    private int money = 0;
    private int image = 0;
    private int productAmount = 0;
    private int passiveIncome = 0;
    // TODO: shares

    void Awake()
    {
        switch (selectedColor)
        {
            case PlayerColor.Red:
                displayColor = new Color(0.9294f, 0.1411f, 0.1411f);
                break;
            case PlayerColor.Green:
                displayColor = new Color(0.1411f, 0.9294f, 0.1411f);
                break;
            case PlayerColor.Blue:
                displayColor = new Color(0.1411f, 0.1411f, 0.9294f);
                break;
            case PlayerColor.Yellow:
                displayColor = Color.yellow;
                break;
        }
    }

}
