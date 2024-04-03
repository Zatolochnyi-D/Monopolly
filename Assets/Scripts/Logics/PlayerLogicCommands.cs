using UnityEngine;

public partial class PlayerLogic : MonoBehaviour
{
    public interface IPlayerCommand
    {
        void Execute(PlayerLogic player);
    }


    public class AlterBalanceCommand : IPlayerCommand
    {
        public int balance;

        public void Execute(PlayerLogic player)
        {
            player.Money += balance;
        }
    }
}
