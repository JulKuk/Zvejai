using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models;

namespace GameServer.Models.Decorator
{
    public abstract class Ginklas
    {
        protected Player player;

        public Ginklas(Player player)
        {
            this.player = player;
        }

        public abstract string Show();

        public abstract Boolean canShow();
    }
}
