using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "TileEffects/TestMessageOnEnter")]
public class TestEnterNotificationSO : TileEffectSO
{
    public string message;

    public override void AlterPlayer(PlayerMovementLogic player)
    {
        Debug.Log(message + " Initiator: " + player.gameObject.name);
    }
}
