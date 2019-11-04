using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models;

namespace GameServer.Models.Decorator
{
    public class Pistoletas : Decorator
    {
        public Pistoletas(Player player) : base(player)
        {

        }

        public override string specificAction()
        {
            return "Pistoletas";
        }
        
        public override Boolean canShow()
        {
            return true;
        }

    }
}
