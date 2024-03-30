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
        public GameObject GetProduct();
    }


    public class PlayerBuilder : IPlayerBuilder
    {
        private GameObject playerTemplate;

        private GameObject actualPlayer;
        private PlayerLogic actualPlayerLogic;

        public string Name { get; private set; }
        public Sprite Visual { get; private set; }

        public PlayerBuilder(GameObject playerPrefab)
        {
            playerTemplate = playerPrefab;
            Reset();
        }

        public void Reset()
        {
            actualPlayer = Instantiate(playerTemplate);
            actualPlayer.SetActive(false);
            actualPlayerLogic = actualPlayer.GetComponent<PlayerLogic>();
        }

        public void SetName(string name)
        {
            actualPlayerLogic.name = name;
            Name = name;
        }

        public void SetNumber(int number)
        {
            actualPlayerLogic.number = number;
        }

        public void SetPosition(int tileID)
        {
            actualPlayerLogic.tileID = tileID;
        }

        public void SetVisuals(PawnVisualsSO pawnVisualsSO)
        {
            actualPlayerLogic.playerVisuals = pawnVisualsSO;
            Visual = pawnVisualsSO.visual;
        }

        private void SetDefaultParameters()
        {
            actualPlayerLogic.money = 100;
            actualPlayerLogic.image = 0;
            actualPlayerLogic.product = 0;
            actualPlayerLogic.passiveIncome = 0;
        }

        public GameObject GetProduct()
        {
            actualPlayer.SetActive(true);
            SetDefaultParameters();
            actualPlayerLogic.Init();
            return actualPlayer;
        }
    }
}
