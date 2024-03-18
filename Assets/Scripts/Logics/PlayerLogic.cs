using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    private TileLogic currentTile;

    void Start()
    {
        currentTile = MapManager.Instance.StartTile;
        transform.position = currentTile.GetTemporalPosition();

        TileLogic.PositionUpdated += OnPositionUpdated;
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
