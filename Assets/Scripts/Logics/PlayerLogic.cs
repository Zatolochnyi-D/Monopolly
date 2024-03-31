using UnityEngine;

public partial class PlayerLogic : MonoBehaviour
{
    // data
    private int tileID;
    private string playerName;
    private PawnVisualsSO playerVisuals;
    private int number;
    private int money;
    private int image;
    private int product = 0;
    private int passiveIncome = 0;
    // TODO: shares

    private TileLogic currentTile;

    public string PlayerName => playerName;
    public Color DisplayColor => playerVisuals.displayColor;

    void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerVisuals.visual;

        currentTile = MapManager.Instance.FindTileByID(tileID);
        transform.position = currentTile.TakePlace(this);

        TileLogic.PositionUpdated += OnPositionUpdated;
        TurnManager.Instance.OnTurnStarted += MovePlayer;
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
        if (TurnManager.Instance.CurrentPlayer == this)
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
}
