using System;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [SerializeField] private TileLogic startTile;
    [SerializeField] private TileLogic alternativeStartTile; // start tile in big circle

    public TileLogic StartTile { get => startTile; }

    void Awake()
    {
        Instance = this;
    }

    public TileLogic FindTileByID(int id)
    {
        TileLogic currentTile = startTile;
        do
        {
            if (id == currentTile.TileID)
            {
                return currentTile;
            }

            currentTile = currentTile.NextTile;
        } while (currentTile.TileID != startTile.TileID);

        currentTile = alternativeStartTile;
        do
        {
            if (id == currentTile.TileID)
            {
                return currentTile;
            }

            currentTile = currentTile.NextTile;
        } while (currentTile.TileID != alternativeStartTile.TileID);

        throw new ArgumentException("No tiles with the given ID.");
    }
}