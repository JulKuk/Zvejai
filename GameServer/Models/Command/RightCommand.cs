using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Command
{
    public class RightCommand : ICommand
    {
        private Player player;

        public RightCommand(Player player)
        {
            this.player = player;
        }
        public void Execute()
        {
            long nextCoord = player.PosX + Convert.ToInt64(player.speed);
            if (nextCoord <= 310)
            {
                player.PosX += Convert.ToInt64(player.speed);
            }
            else
            {
                long diff = 310 - player.PosX;
                player.PosX += diff;
            }
        }

        public void undo(long diff)
        {
            throw new NotImplementedException();
        }
    }
}
