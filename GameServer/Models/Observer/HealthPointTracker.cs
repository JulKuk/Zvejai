using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Observer
{
    public interface HealthPointTracker
    {
        void Update(Player players, int hp);
    }
}
