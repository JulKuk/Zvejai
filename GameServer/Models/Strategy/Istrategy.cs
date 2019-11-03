using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Strategy
{
    public interface Istrategy
    {
        void Move(Player entity);

    }
}
