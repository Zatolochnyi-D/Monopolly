using UnityEngine;

public class TestPlayerCreatorManager : MonoBehaviour
{
    [SerializeField] private PawnVisualsSO[] visuals;
    [SerializeField] private string[] names;

    void Awake()
    {
        PlayerLogic.PlayerBuilder[] builders = new PlayerLogic.PlayerBuilder[1];

        for (int i = 0; i < builders.Length; i++)
        {
            builders[i] = new();
            builders[i].SetName(names[i]);
            builders[i].SetPosition(0);
            builders[i].SetVisuals(visuals[i]);
        }

        TurnManager.playerBuilders = builders;
    }
}
