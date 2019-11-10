using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Decorator
{
    public class Snaiperis : Decorator
    {
        public Snaiperis(Player player) : base(player)
        {

        }

        public override string specificAction()
        {
            return "Snaiperis";
        }

        public override Boolean canShow()
        {
            return true;
        }
    }
}
