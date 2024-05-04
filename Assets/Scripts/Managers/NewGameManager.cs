using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    public static NewGameManager Instance { get; private set; }

    public event Action OnPlayerAdded;
    public event Action<int> OnPlayerRemoved;

    private List<PlayerLogic.IPlayerBuilder> builders;

    public List<PlayerLogic.IPlayerBuilder> PlayerList => builders;
    public bool IsMinPlayersReached => builders.Count == 2;
    public bool IsMaxPlayersReached => builders.Count == 4;

    void Awake()
    {
        Instance = this;
        Reset();
    }

    public void Reset()
    {
        builders = new() { new PlayerLogic.PlayerBuilder(), new PlayerLogic.PlayerBuilder() };
    }

    public void AddPlayer()
    {
        builders.Add(new PlayerLogic.PlayerBuilder());
        OnPlayerAdded?.Invoke();
    }

    public void RemovePlayer(int index)
    {
        builders.RemoveAt(index);
        OnPlayerRemoved?.Invoke(index);
    }

    public void CreateGame()
    {
        string[] names = NameSelectionManager.Instance.GetNames();
        PawnVisualsSO[] visuals = PawnSelectionManager.Instance.GetVisuals();
        List<int> numbers = new() { 1, 2, 3, 4, 5 };
        for (int i = 0; i < builders.Count; i++)
        {
            builders[i].SetName(names[i]);
            builders[i].SetVisuals(visuals[i]);
            int index = UnityEngine.Random.Range(0, numbers.Count);
            builders[i].SetNumber(numbers[index]);
            numbers.RemoveAt(index);
        }

        TurnManager.playerBuilders = builders.ToArray();
        TurnManager.loadFromSnapshot = null;
        Loader.LoadScene(Loader.Scenes.Game);
    }
}