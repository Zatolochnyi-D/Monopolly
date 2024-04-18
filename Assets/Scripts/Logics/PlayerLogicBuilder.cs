using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public partial class PlayerLogic
{
    public interface IPlayerBuilder
    {
        public void SetName(string name);
        public void SetVisuals(PawnVisualsSO pawnVisualsSO);
        public void SetNumber(int number);

        public void SetPosition(int tileID);
        public void SetMoneyAndImage(int money, int image);
        public void SetProduction(int production, int passiveProduct);
        public void SetShares(PlayerShares shares);
        public void SetDirectors(IEnumerable<Director> directors);

        public void Reset();
        public GameObject GetProduct(GameObject playerPrefab);
    }


    public class PlayerBuilder : IPlayerBuilder
    {
        private string name;
        private PawnVisualsSO visuals;
        private int number;

        private int tileID;
        private int money;
        private int image;
        private int production;
        private int passiveProduct;
        private PlayerShares shares;
        private ObservableCollection<Director> directors;

        public PlayerBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            // set every time
            name = "";
            visuals = null;
            number = 0;

            // set only on load save
            tileID = 0;
            money = 100;
            image = 0;
            production = 0;
            passiveProduct = 0;
            shares = new();
            directors = new();
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetVisuals(PawnVisualsSO pawnVisualsSO)
        {
            visuals = pawnVisualsSO;
        }

        public void SetNumber(int number)
        {
            this.number = number;
        }

        public void SetPosition(int tileID)
        {
            this.tileID = tileID;
        }

        public void SetMoneyAndImage(int money, int image)
        {
            this.money = money;
            this.image = image;
        }

        public void SetProduction(int production, int passiveProduct)
        {
            this.production = production;
            this.passiveProduct = passiveProduct;
        }

        public void SetShares(PlayerShares shares)
        {
            this.shares = shares;
        }

        public void SetDirectors(IEnumerable<Director> directors)
        {
            this.directors = new(directors);
        }

        public GameObject GetProduct(GameObject playerPrefab)
        {
            GameObject player = Instantiate(playerPrefab);

            PlayerLogic script = player.GetComponent<PlayerLogic>();
            script.playerName = name;
            script.playerVisuals = visuals != null ? visuals : throw new ArgumentException("No visuals provided.");
            script.number = number;
            script.tileID = tileID;
            script.money = money;
            script.image = image;
            script.production = production;
            script.passiveProduct = passiveProduct;
            script.playerShares = shares;
            script.playerDirectors = directors;

            return player;
        }
    }
}
