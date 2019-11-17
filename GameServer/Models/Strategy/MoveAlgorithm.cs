using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Strategy
{
    public class MoveAlgorithm : Istrategy
    {
        public void action(Player entity, float speed)
        {
            entity.speed = speed;
        }
    }
}
