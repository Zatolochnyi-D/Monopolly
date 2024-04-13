using System.Collections.Generic;
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
        public static List<int> availableNumbers = new() { 1, 2, 3, 4, 5 };

        public static void ResetNumbers()
        {
            availableNumbers = new() { 1, 2, 3, 4, 5 };
        }

        private string name;
        private int tileID;
        private PawnVisualsSO pawnVisuals;
        private int number;
        private int money;
        private int image;
        private int production;

        private PlayerShares shares;

        public PlayerBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            name = "";
            tileID = 0;
            pawnVisuals = null;

            int selectedNumber = Random.Range(0, availableNumbers.Count);
            number = availableNumbers[selectedNumber];
            availableNumbers.RemoveAt(selectedNumber);

            money = 100;
            image = 1;
            production = 10000;
            shares = new() { Airlines = 45, BookPublisher = 60, BuildingAgency = 51, TVCompany = 10, CarManufacturer = 100 };
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
            script.production = production;
            
            script.playerShares = shares;

            if (pawnVisuals == null)
            {
                Debug.LogError("Assigning of pawn visuals is necessary");
            }

            script.playerVisuals = pawnVisuals;

            return player;
        }
    }
}
