using UnityEngine;

public class TestPlayerCreatorManager : MonoBehaviour
{
    [SerializeField] private PawnVisualsSO[] visuals;
    [SerializeField] private string[] names;

    void Awake()
    {
        PlayerLogic.PlayerBuilder[] builders = new PlayerLogic.PlayerBuilder[4];

        for (int i = 0; i < 4; i++)
        {
            builders[i] = new();
            builders[i].SetName(names[i]);
            builders[i].SetPosition(0);
            builders[i].SetNumber(3);
            builders[i].SetVisuals(visuals[i]);
        }

        TurnManager.playerBuilders = builders;
    }
}