using UnityEngine;

public class TileLogic : MonoBehaviour
{
    [SerializeField] private int tileID;
    [SerializeField] private TileLogic nextTile;
    [SerializeField] private PlayerLogic.TileCommandSO commandSO;

    public int TileID => tileID;
    public TileLogic NextTile => nextTile;

    public Vector3 GetPosition()
    {
        return transform.position + new Vector3(0, 0, -1);
    }

    public void AlterPlayer(PlayerLogic player)
    {
        commandSO?.Alter(player);
    }
}
