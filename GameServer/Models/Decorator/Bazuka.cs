using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models;

namespace GameServer.Models.Decorator
{
    public class Bazuka : Decorator
    {
        public Bazuka(Player player) : base(player)
        {

        }

        public override string specificAction()
        {
            return "Bazuka";
        }

        public override Boolean canShow()
        {
            return true;
        }
    }
}
