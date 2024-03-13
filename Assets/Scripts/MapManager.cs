using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [SerializeField] private GameObject startTile;

    public GameObject StartTile { get => startTile; }

    void Awake()
    {
        Instance = this;
    }
}
