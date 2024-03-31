using UnityEngine;

public partial class PlayerLogic
{
    public interface IPlayerBuilder
    {
        public void Reset();
        public void SetName(string name);
        public void SetNumber(int number);
        public void SetVisuals(PawnVisualsSO pawnVisualsSO);
        public void SetPosition(int tileID);
        public GameObject GetProduct(GameObject playerPrefab);
    }


    public class PlayerBuilder : IPlayerBuilder
    {
        private string name;
        private int tileID;
        private PawnVisualsSO pawnVisuals;
        private int number;
        private int money;
        private int image;

        public PlayerBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            name = "";
            tileID = 0;
            pawnVisuals = null;
            number = Random.Range(1, 6);
            money = 100;
            image = 0;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetNumber(int number)
        {
            this.number = number;
        }

        public void SetPosition(int tileID)
        {
            this.tileID = tileID;
        }

        public void SetVisuals(PawnVisualsSO pawnVisualsSO)
        {
            pawnVisuals = pawnVisualsSO;
        }

        public GameObject GetProduct(GameObject playerPrefab)
        {
            GameObject player = Instantiate(playerPrefab);

            PlayerLogic script = player.GetComponent<PlayerLogic>();

            script.playerName = name;
            script.tileID = tileID;
            script.number = number;
            script.money = money;
            script.image = image;

            if (pawnVisuals == null)
            {
                Debug.LogError("Assigning of pawn visuals is necessary");
            }

            script.playerVisuals = pawnVisuals;

            return player;
        }
    }
}
