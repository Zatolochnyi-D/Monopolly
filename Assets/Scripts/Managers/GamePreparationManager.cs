using UnityEngine;

public class GamePreparationManager : MonoBehaviour
{
    public static GamePreparationManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
}
