using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.AbstractFactory
{
    public class PlayerFactory : AbstractFactory
    {
        public override Entity CreatePlayer()
        {
            return new Player(1, 100, "Julius");
        }
    }
}
