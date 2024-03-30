using System;
using System.Collections.Generic;
using UnityEngine;

public class NewGameOptionsManager : MonoBehaviour
{
    public static NewGameOptionsManager Instance { get; private set; }

    private PlayerListOptionsModule playerList = new();

    public PlayerListOptionsModule PlayerList => playerList;

    void Awake()
    {
        Instance = this;
    }

    public void Reset()
    {
        playerList = new();
    }
}


public class PlayerListOptionsModule
{
    public struct Player
    {
        public string name;
    }

    public event Action OnPlayersUpdated;

    private readonly int maxPlayers = 4;
    private readonly int minPlayers = 2;
    private readonly List<Player> players = new();

    private readonly Dictionary<string, bool> defaultNames = new()
    {
        { "Player4", false },
        { "Player3", false },
        { "Player2", false },
        { "Player1", false },
    };

    public int MaxPlayers => maxPlayers;
    public int MinPlayers => minPlayers;
    public List<Player> Players => players;
    public bool IsMaxPlayersReached => players.Count == maxPlayers;
    public bool IsMinPlayersReached => players.Count == minPlayers;

    public PlayerListOptionsModule()
    {
        for (int i = 0; i < minPlayers; i++)
        {
            Player player = new()
            {
                name = GetDefaultName(),
            };

            players.Add(player);
        }
    }

    private string GetDefaultName()
    {
        string name = "";
        foreach (var kvPair in defaultNames)
        {
            if (!kvPair.Value)
            {
                name = kvPair.Key;
            }
        }

        defaultNames[name] = true;

        return name;
    }

    private void FreeDefaultName(string name)
    {
        if (defaultNames.ContainsKey(name))
        {
            defaultNames[name] = false;
        }
    }

    public void AddPlayer()
    {
        Player player = new() 
        {
            name = GetDefaultName(),
        };       

        players.Add(player);
        OnPlayersUpdated?.Invoke();
    }

    public void RemovePlayer(int index)
    {
        FreeDefaultName(players[index].name);

        players.RemoveAt(index);
        OnPlayersUpdated?.Invoke();
    }
}