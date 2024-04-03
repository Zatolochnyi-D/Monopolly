using UnityEngine;

public partial class PlayerLogic : MonoBehaviour
{
    public interface IPlayerCommand
    {
        void Execute(PlayerLogic player);
    }


    public class AlterBalanceAdditively : IPlayerCommand
    {
        public int balance;

        public void Execute(PlayerLogic player)
        {
            player.Money += balance;
        }
    }


    public class DecreaseImageMultiplyAdditively : IPlayerCommand
    {
        public float coeffitient;

        public void Execute(PlayerLogic player)
        {
            int imageToSubstract;
            if (player.Image > 0)
            {
                imageToSubstract = Mathf.CeilToInt(player.Image * coeffitient);
            }
            else
            {
                imageToSubstract = 1;
            }
            player.Image -= imageToSubstract;
        }
    }
}
