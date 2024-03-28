using System;
using System.Collections.Generic;
using UnityEngine;

public class NewGameOptionsManager : MonoBehaviour
{
    public struct NewPlayerData
    {
        public Sprite sprite;
        public string name;
    }

    public static NewGameOptionsManager Instance { get; private set; }

    public event Action OnPlayersUpdated;

    private readonly int maxPlayerAmount = 4;
    private readonly int minPlayerAmount = 2;
    private readonly List<NewPlayerData> playerDatas = new();

    private readonly Dictionary<string, bool> defaultNames = new()
    {
        { "Player4", false },
        { "Player3", false },
        { "Player2", false },
        { "Player1", false },
    };

    public int MaxPlayerAmount => maxPlayerAmount;
    public int MinPlayerAmount => minPlayerAmount;
    public List<NewPlayerData> PlayerDatas => playerDatas;
    public bool IsMaxPlayerLimitReached => playerDatas.Count == maxPlayerAmount;
    public bool IsMinPlayerLimitReached => playerDatas.Count == minPlayerAmount;

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < minPlayerAmount; i++)
        {
            NewPlayerData n = new();
            n.name = GetDefaultName();

            playerDatas.Add(n);
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
        NewPlayerData n = new();
        n.name = GetDefaultName();

        playerDatas.Add(n);
        OnPlayersUpdated?.Invoke();
    }

    public void RemovePlayer(int index)
    {
        FreeDefaultName(playerDatas[index].name);

        playerDatas.RemoveAt(index);
        OnPlayersUpdated?.Invoke();
    }
}
