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