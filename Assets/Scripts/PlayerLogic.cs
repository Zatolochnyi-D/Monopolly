using System;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public abstract class TileCommandSO : ScriptableObject
    {
        public abstract void Alter(PlayerLogic player);
    }

    [CreateAssetMenu(menuName = "TileCommands/Tile1")]
    public class Tile1SO : TileCommandSO
    {
        public int tileID;

        [HideInInspector] public TileLogic tileTo;

        public override void Alter(PlayerLogic player)
        {
            if (tileTo == null)
            {
                tileTo = MapManager.Instance.FindTileByID(tileID);
            }

            player.currentTile = tileTo;
            player.transform.position = tileTo.GetPosition();
            tileTo.AlterPlayer(player);
        }
    }


    private TileLogic currentTile;

    void Start()
    {
        currentTile = MapManager.Instance.StartTile;
        transform.position = currentTile.GetPosition();

        DiceManager.Instance.DiceRolled += OnDiceRolled;
    }

    private void OnDiceRolled(int rolledNumber)
    {
        for (int i = 0; i < rolledNumber; i++)
        {
            currentTile = currentTile.NextTile;
        }

        transform.position = currentTile.GetPosition();
        currentTile.AlterPlayer(this);
    }
}
