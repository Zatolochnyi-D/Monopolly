using UnityEngine;

public class DiceThrowingManager : MonoBehaviour
{
    public static DiceThrowingManager Instance;

    [SerializeField] private DiceLogic firstDice;
    [SerializeField] private DiceLogic secondDice;

    void Awake()
    {
        Instance = this;
    }
}
