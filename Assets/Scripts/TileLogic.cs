using UnityEngine;

public class TileLogic : MonoBehaviour
{
    [SerializeField] private TileLogic nextTile;

    public TileLogic NextTile { get => nextTile; }

    public Vector3 GetPosition()
    {
        return transform.position + new Vector3(0, 0, -1);
    }
}
