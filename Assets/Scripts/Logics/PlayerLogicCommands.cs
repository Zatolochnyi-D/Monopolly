using System;
using UnityEngine;

public partial class PlayerLogic : MonoBehaviour
{
    public abstract class PlayerCommandParams { }


    public class SimpleIntegerParam : PlayerCommandParams
    {
        public int integer;
    }


    public class SimpleFloatParam : PlayerCommandParams
    {
        public float floating;
    }


    public class DoubleIntegerParam : PlayerCommandParams
    {
        public int first;
        public int second;
    }


    public class SimpleTileParam : PlayerCommandParams
    {
        public TileLogic tile;
    }


    public class TileIntegersParam: PlayerCommandParams
    {
        public SimpleTileParam tile = new();
        public DoubleIntegerParam integers = new();
    }


    public abstract class PlayerCommand
    {
        protected PlayerLogic player;

        public virtual void SetReceiver(PlayerLogic player)
        {
            this.player = player;
        }

        public abstract void Execute(PlayerCommandParams parameters);

        protected T Validate<T>(PlayerCommandParams parameters) where T : PlayerCommandParams
        {
            T param = parameters as T;

            if (param == null) throw new ArgumentException("Incorrect parameter in tile interaction");

            return param;
        }
    }


    public class AddBalanceCommand : PlayerCommand
    {
        public override void Execute(PlayerCommandParams parameters)
        {
            var param = Validate<SimpleIntegerParam>(parameters);

            player.Money += param.integer;
        }
    }


    public class AddImageCommand : PlayerCommand
    {
        public override void Execute(PlayerCommandParams parameters)
        {
            var param = Validate<SimpleIntegerParam>(parameters);

            player.Image += param.integer;
        }
    }


    public class MultiplyAddWithCapImageCommand : PlayerCommand
    {
        private PlayerCommand alterImageCommand = new AddImageCommand();

        public override void SetReceiver(PlayerLogic player)
        {
            base.SetReceiver(player);
            alterImageCommand.SetReceiver(player);
        }

        public override void Execute(PlayerCommandParams parameters)
        {
            var param = Validate<SimpleFloatParam>(parameters);

            SimpleIntegerParam intToAdd;
            if (player.Image > 0)
            {
                intToAdd = new() { integer = Mathf.CeilToInt(player.Image * param.floating) };
            }
            else
            {
                intToAdd = new() { integer = 1 * Math.Sign(param.floating) };
            }

            alterImageCommand.Execute(intToAdd);
        }
    }


    public class AddImagePercentFromHighestCommand : PlayerCommand
    {
        private PlayerCommand alterImageCommand = new AddImageCommand();

        public override void SetReceiver(PlayerLogic player)
        {
            base.SetReceiver(player);
            alterImageCommand.SetReceiver(player);
        }

        public override void Execute(PlayerCommandParams parameters)
        {
            var param = Validate<SimpleFloatParam>(parameters);

            int highestImage = TurnManager.Instance.HighestImageExcludeCurrentPlayer;
            SimpleIntegerParam intToAdd = new();
            if (highestImage > 0)
            {
                intToAdd.integer = Mathf.CeilToInt(highestImage * param.floating);
            }
            else
            {
                intToAdd.integer = Mathf.CeilToInt(Mathf.Abs(highestImage) * param.floating);
            }

            alterImageCommand.Execute(intToAdd);
        }
    }


    public class MultiplyAddWithCapBalanceCommand : PlayerCommand
    {
        private PlayerCommand alterBalanceCommand = new AddBalanceCommand();

        public override void SetReceiver(PlayerLogic player)
        {
            base.SetReceiver(player);
            alterBalanceCommand.SetReceiver(player);
        }

        public override void Execute(PlayerCommandParams parameters)
        {
            var param = Validate<SimpleFloatParam>(parameters);

            SimpleIntegerParam intToAdd = new();
            if (player.Money > 0)
            {
                intToAdd.integer = Mathf.FloorToInt(player.Money * param.floating);
            }
            else
            {
                intToAdd.integer = 0;
            }

            alterBalanceCommand.Execute(intToAdd);
        }
    }

    public class ImageMoneyExchangeCommand : PlayerCommand
    {
        private PlayerCommand alterImageCommand = new AddImageCommand();
        private PlayerCommand alterBalanceCommand = new AddBalanceCommand();

        public override void SetReceiver(PlayerLogic player)
        {
            base.SetReceiver(player);
            alterImageCommand.SetReceiver(player);
            alterBalanceCommand.SetReceiver(player);
        }

        public override void Execute(PlayerCommandParams parameters)
        {
            var param = Validate<DoubleIntegerParam>(parameters);

            alterImageCommand.Execute(new SimpleIntegerParam() { integer = param.first });
            alterBalanceCommand.Execute(new SimpleIntegerParam() { integer = param.second });
        }
    }


    public class MovePlayerCommand : PlayerCommand
    {
        public override void Execute(PlayerCommandParams parameters)
        {
            var param = Validate<SimpleTileParam>(parameters);

            player.currentTile = param.tile;
            player.TakeTile();
        }
    }


    public class MovePlayerAndChangeStatsCommand : PlayerCommand
    {
        private PlayerCommand alterStatsCommand = new ImageMoneyExchangeCommand();
        private PlayerCommand movePlayer = new MovePlayerCommand();

        public override void SetReceiver(PlayerLogic player)
        {
            base.SetReceiver(player);
            alterStatsCommand.SetReceiver(player);
            movePlayer.SetReceiver(player);
        }

        public override void Execute(PlayerCommandParams parameters)
        {
            var param = Validate<TileIntegersParam>(parameters);

            alterStatsCommand.Execute(param.integers);

            if (param.tile.tile != null)
            {
                movePlayer.Execute(param.tile);
            }
        }
    }
}
