using System.Collections.Generic;
using UnityEngine;

public class NameSelectionManager : MonoBehaviour
{
    public static NameSelectionManager Instance { get; private set; }

    private readonly Dictionary<string, bool> defaultNames = new()
    {
        { "Player1", false },
        { "Player2", false },
        { "Player3", false },
        { "Player4", false },
    };

    private List<string> names;

    public List<string> Names => names;

    void Awake()
    {
        Instance = this;
        names = new();
    }

    private string GetDefaultName()
    {
        string name = "";
        foreach (var kvPair in defaultNames)
        {
            if (!kvPair.Value)
            {
                name = kvPair.Key;
                break;
            }
        }
        defaultNames[name] = true;

        return name;
    }

    private void FreeDefaultName(string name)
    {
        if (defaultNames.ContainsKey(name)) defaultNames[name] = false;
    }

    public int AddPlayer()
    {
        names.Add(GetDefaultName());
        return names.Count - 1;
    }

    public void RemovePlayer(int index)
    {
        FreeDefaultName(names[index]);
        names.RemoveAt(index);
    }

    public void ChangeName(int index, string newName)
    {
        FreeDefaultName(names[index]);
        names[index] = newName;
    }
}
