using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Strategy
{
    public class StopStrategy : Istrategy
    {
        public void action(Player entity)
        {
            entity.speed = 0.0f;
        }
    }
}
