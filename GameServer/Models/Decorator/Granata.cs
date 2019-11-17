using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models;

namespace GameServer.Models.Decorator
{
    public class Granata : Decorator
    {
        public Granata(Player player) : base(player)
        {

        }

        public override string specificAction()
        {
            return "Granata";
        }

        public override Boolean canShow()
        {
            return true;
        }
    }
}
