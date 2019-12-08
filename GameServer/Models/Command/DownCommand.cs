using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Command
{
    public class DownCommand : ICommand
    {
        private Player player;

        public DownCommand(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {
            long nextCoord = player.PosY + Convert.ToInt64(player.speed);
            if (nextCoord <= 310)
            {
                player.PosY += Convert.ToInt64(player.speed);
            }
            else
            {
                long diff = 310 - player.PosY;
                undo(diff);
            }
        }

        public void undo(long diff)
        {
            player.PosY += diff;
        }
    }
}
