using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Command
{
    public class LeftCommand : ICommand
    {
        private Player player;

        public LeftCommand(Player player)
        {
            this.player = player;
        }
        public void Execute()
        {
            long nextCoord = player.PosX - Convert.ToInt64(player.speed);
            if (nextCoord >= 5)
            {
                player.PosX -= Convert.ToInt64(player.speed);
            }
            else
            {
                long diff = player.PosX - 5;
                undo(diff);
            }
        }

        public void undo(long diff)
        {
            player.PosX -= diff;
        }
    }
}
