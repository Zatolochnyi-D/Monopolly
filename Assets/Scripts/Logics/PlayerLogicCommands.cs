using System;
using UnityEngine;

public partial class PlayerLogic : MonoBehaviour
{
    public class PlayerCommandParams { }


    public class SimpleIntegerParam : PlayerCommandParams
    {
        public int integer;
    }

    public class SimpleFloatParam : PlayerCommandParams
    {
        public float floating;
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
        private AddImageCommand alterImageCommand = new();

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
}
