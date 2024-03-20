using UnityEngine;

public abstract class TileEffectSO : ScriptableObject
{
    public abstract void AlterPlayer(PlayerMovementLogic player);
}