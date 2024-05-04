using UnityEngine;

public static class Saver
{
    public static string SerializePlayer(PlayerLogic.PlayerSnapshot snapshot)
    {
        return JsonUtility.ToJson(snapshot);
    }

    public static string SerializeGame(TurnManager.GameSnapshot snapshot)
    {
        return JsonUtility.ToJson(snapshot);
    }

    public static TurnManager.GameSnapshot DeserializeGame(string serializedSnapshot)
    {
        return JsonUtility.FromJson<TurnManager.GameSnapshot>(serializedSnapshot);
    }

    public static PlayerLogic.PlayerSnapshot DeserializePlayer(string serializedSnapshot)
    {
        return JsonUtility.FromJson<PlayerLogic.PlayerSnapshot>(serializedSnapshot);
    }
}
