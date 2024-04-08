using System;
using System.Linq;
using UnityEngine;

public class TileLogic : MonoBehaviour
{
    public static event Action<int> PositionUpdated;

    [SerializeField] private int tileID;
    [SerializeField] private TileLogic nextTile;
    [SerializeField] private Transform[] playerPositions;

    [Header("Tile interactions")]
    [SerializeField] private InteractionUI tileInteractionDialog;

    private PlayerLogic[] players = new PlayerLogic[0];
    private int tileCapacityLevel = 0;

    public int TileID => tileID;
    public TileLogic NextTile => nextTile;

    public Vector3 GetTemporalPosition()
    {
        return playerPositions[0].GetChild(0).position;
    }

    public Vector3 TakePlace(PlayerLogic player)
    {
        // look for place on tile for a new player
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null)
            {
                // if place is empty, place new player here and return position

                players[i] = player;
                return playerPositions[tileCapacityLevel - 1].GetChild(i).position;
            }
        }

        // if there is not enough rooms, raise tile capacity and place new player

        tileCapacityLevel++;
        PlayerLogic[] newPlayers = new PlayerLogic[tileCapacityLevel];
        players.CopyTo(newPlayers, 0);

        newPlayers[^1] = player;
        players = newPlayers;
        PositionUpdated?.Invoke(tileID);
        return playerPositions[tileCapacityLevel - 1].GetChild(tileCapacityLevel - 1).position;
    }

    public Vector3 GetUpdatedPosition(PlayerLogic player)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == player)
            {
                return playerPositions[tileCapacityLevel - 1].GetChild(i).position;
            }
        }

        throw new Exception("Something went wrong while updating players position");
    }

    public void ReleasePlace(PlayerLogic player)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == player)
            {
                players[i] = null;
                break;
            }
        }

        if (players.All(x => x == null))
        {
            tileCapacityLevel = 0;
            players = new PlayerLogic[tileCapacityLevel];
        }
    }

    public void Interact()
    {
        if (tileInteractionDialog != null)
        {
            tileInteractionDialog.Interact();
        }
        else
        {
            TurnManager.Instance.EndTurn();
        }
    }
}
