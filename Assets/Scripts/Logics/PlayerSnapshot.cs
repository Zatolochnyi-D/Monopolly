using System.Linq;

public partial class PlayerLogic
{
    public class PlayerSnapshot
    {
        public string name;
        public int tileId;
        public int visualId;
        public int number;
        public int money;
        public int image;
        public int production;
        public int passiveProduct;
        public PlayerShares shares;
        public Director[] directors;

        public PlayerSnapshot(PlayerLogic player)
        {
            name = player.playerName;
            tileId = player.currentTile.TileID;
            visualId = PawnSelectionManager.Instance.GetIdByVisual(player.playerVisuals.visual);
            number = player.number;
            money = player.money;
            image = player.image;
            production = player.production;
            passiveProduct = player.passiveProduct;
            shares = player.Shares;
            directors = player.Directors.ToArray();
        }

        public PlayerBuilder LoadToBuilder()
        {
            PlayerBuilder builder = new();
            builder.SetName(name);
            builder.SetVisuals(PawnSelectionManager.Instance.GetVisualById(visualId));
            builder.SetNumber(number);
            builder.SetPosition(tileId);
            builder.SetMoneyAndImage(money, image);
            builder.SetProduction(production, passiveProduct);
            builder.SetShares(shares);
            builder.SetDirectors(directors);

            return builder;
        }
    }
}