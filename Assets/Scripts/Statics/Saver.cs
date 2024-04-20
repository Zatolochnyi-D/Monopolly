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

    public static void PrintGameJson(TurnManager.GameSnapshot snapshot)
    {
        Debug.Log(JsonUtility.ToJson(snapshot));
    }
}
