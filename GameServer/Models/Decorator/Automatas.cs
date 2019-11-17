using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models;

namespace GameServer.Models.Decorator
{
    public class Automatas : Decorator
    {
        public Automatas(Player player) : base(player)
        {

        }

        public override string specificAction()
        {
            return "Automatas";
        }

        public override Boolean canShow()
        {
            return true;
        }
    }
}
