using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Observer
{
    public class Observer : HealthPointTracker
    {
        public void Update(Player players, int hp)
        {
            if (Math.Abs(hp) > 0)
            {
                players.UpdateHealth(hp);
            }
        }
    }
}
