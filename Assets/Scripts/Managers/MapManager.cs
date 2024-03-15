using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [SerializeField] private TileLogic startTile;

    public TileLogic StartTile { get => startTile; }

    void Awake()
    {
        Instance = this;
    }
}
