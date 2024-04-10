using System;
using UnityEngine;

public partial class PlayerLogic : MonoBehaviour
{
    public abstract class PlayerCommandParams { }


    public class SimpleIntegerParam : PlayerCommandParams
    {
        public int integer;
    }


    public class SimpleTileParam : PlayerCommandParams
    {
        public TileLogic tile;
    }


    public class SimpleSharesParam : PlayerCommandParams
    {
        public PlayerShares shares;
    }


    public abstract class PlayerCommand
    {
        protected PlayerLogic targetPlayer;
        protected PlayerCommandParams parameters;
        protected PlayerCommand nextCommand;

        public virtual PlayerCommandParams Parameters { get => parameters; set => parameters = value; }
        public virtual PlayerCommand NextCommand { get => nextCommand; set => nextCommand = value; }
        public virtual PlayerLogic TargetPlayer
        {
            set 
            {
                targetPlayer = value;
                if (nextCommand != null) nextCommand.TargetPlayer = value;
            } 
        }

        public virtual void Execute()
        {
            nextCommand?.Execute();
        }

        protected T Validate<T>(PlayerCommandParams parameters) where T : PlayerCommandParams
        {
            T param = parameters as T;

            if (param == null) throw new ArgumentException("Incorrect parameter in tile interaction");

            return param;
        }
    }


    public class AlterBalanceCommand : PlayerCommand
    {
        public override void Execute()
        {
            var param = Validate<SimpleIntegerParam>(parameters);

            targetPlayer.Money += param.integer;

            base.Execute();
        }
    }


    public class AlterImageCommand : PlayerCommand
    {
        public override void Execute()
        {
            var param = Validate<SimpleIntegerParam>(parameters);

            targetPlayer.Image += param.integer;

            base.Execute();
        }
    }


    public class MovePlayerCommand : PlayerCommand
    {
        public override void Execute()
        {
            var param = Validate<SimpleTileParam>(parameters);

            if (param.tile != null)
            {
                targetPlayer.currentTile = param.tile;
                targetPlayer.TakeTile();
            }

            base.Execute();
        }
    }


    public class AlterPassiveIncomeCommand : PlayerCommand
    {
        public override void Execute()
        {
            var param = Validate<SimpleIntegerParam>(parameters);

            targetPlayer.PassiveProduct += param.integer;

            base.Execute();
        }
    }


    public class AlterProductionCommand : PlayerCommand
    {
        public override void Execute()
        {
            var param = Validate<SimpleIntegerParam>(parameters);

            targetPlayer.Production += param.integer;

            base.Execute();
        }
    }


    public class AlterPlayerShares : PlayerCommand
    {
        public override void Execute()
        {
            var param = Validate<SimpleSharesParam>(parameters);

            targetPlayer.Shares += param.shares;

            base.Execute();
        }
    }
}
