using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    private TileLogic currentTile;

    void Start()
    {
        currentTile = MapManager.Instance.StartTile;
        transform.position = currentTile.GetPosition();
    }

    public void MovePlayer(int rolledNumber)
    {
        for (int i = 0; i < rolledNumber; i++)
        {
            currentTile = currentTile.NextTile;
        }

        transform.position = currentTile.GetPosition();
        currentTile.AlterPlayer(this);
    }
}
