using System;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    public static NewGameManager Instance { get; private set; }

    public event Action OnPlayerAdded;
    public event Action<int> OnPlayerRemoved;
    private List<PlayerLogic.PlayerBuilder> builders;

    public List<PlayerLogic.PlayerBuilder> PlayerList => builders;
    public bool IsMinPlayersReached => builders.Count == 2;
    public bool IsMaxPlayersReached => builders.Count == 4;

    void Awake()
    {
        Instance = this;
        Reset();
    }

    public void Reset()
    {
        builders = new() { new(), new() };
    }

    public void AddPlayer()
    {
        builders.Add(new());
        OnPlayerAdded?.Invoke();
    }

    public void RemovePlayer(int index)
    {
        builders.RemoveAt(index);
        OnPlayerRemoved?.Invoke(index);
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