using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Strategy
{
    public class MoveAlgorithm : Istrategy
    {
        public void action(Player entity)
        {
            entity.speed = 10.0f;
        }
    }
}
