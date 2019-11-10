using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Builder
{
    public class PlayerBuilder
    {
        private Player player;

        public PlayerConfigurator startPlayer()
        {
            player = new Player();
            return new PlayerConfigurator(player);
        }

        //public PlayerConfigurator AddWeapons()
        //{

        //}
    }
}
