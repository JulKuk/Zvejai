using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Strategy
{
    public class StopStrategy : Istrategy
    {
        public void Move(Player entity)
        {
            entity.speed = 0.0f;
        }
    }
}
