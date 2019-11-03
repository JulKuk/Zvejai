using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Strategy
{
    public class MoveAlgorithm : Istrategy
    {
        public void Move(Player entity)
        {
            entity.speed = 10.0f;
        }
    }
}
