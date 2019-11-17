using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Command
{
    public class UpCommand : ICommand
    {
        private Player player;

        public UpCommand(Player player)
        {
            this.player = player;
        }
        public void Execute()
        {
            long nextCoord = player.PosY - Convert.ToInt64(player.speed);
            if (nextCoord >= 5)
            {
                player.PosY -= Convert.ToInt64(player.speed);
            }
            else
            {
                long diff = player.PosY - 5;
                player.PosY -= diff;
            }
        }

        public void undo(long diff)
        {
            throw new NotImplementedException();
        }
    }
}
