using UnityEngine;

[CreateAssetMenu(menuName = "TileEffects/TestMessageOnEnter")]
public class TestEnterNotificationSO : TileEffectSO
{
    public string message;

    public override void AlterPlayer(PlayerLogic player)
    {
        Debug.Log(message);
    }
}
