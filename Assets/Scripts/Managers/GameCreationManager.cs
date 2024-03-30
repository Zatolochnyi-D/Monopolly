using UnityEngine;

public class GameCreationManager : MonoBehaviour
{
    public static GameCreationManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void Reset()
    {
        // clear all before entering the new game page
    }
}
