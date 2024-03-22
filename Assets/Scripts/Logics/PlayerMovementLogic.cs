using UnityEngine;

public class PlayerMovementLogic : MonoBehaviour
{
    private TileLogic currentTile;

    void Start()
    {
        TileLogic.PositionUpdated += OnPositionUpdated;

        currentTile = MapManager.Instance.StartTile;
        transform.position = currentTile.TakePlace(this);
    }

    private void OnPositionUpdated(int id)
    {
        if (id == currentTile.TileID)
        {
            transform.position = currentTile.GetUpdatedPosition(this);
        }
    }

    public void MovePlayer(int rolledNumber)
    {
        currentTile.ReleasePlace(this);

        for (int i = 0; i < rolledNumber; i++)
        {
            currentTile = currentTile.NextTile;
        }

        transform.position = currentTile.TakePlace(this);
        currentTile.AlterPlayer(this);
    }
}