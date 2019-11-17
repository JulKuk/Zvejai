using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models;

namespace GameServer.Models.Decorator
{
    public abstract class Decorator : Ginklas
    {
        public Decorator(Player player/*, Ginklas inner*/) : base(player)
        {
            //this.innerElement = inner;
        }

        //private Ginklas innerElement;

        public sealed override string Show()
        {
            return "Decorator object: " + specificAction() /*+ " ant " + this.innerElement.Show() */+ "; Player name: " + player.Name;
        }

        public abstract string specificAction();

    }
}
