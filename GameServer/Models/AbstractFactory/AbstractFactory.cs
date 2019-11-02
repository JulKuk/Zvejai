using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.AbstractFactory
{
    public abstract class AbstractFactory
    {
        public abstract Entity CreatePlayer();

    }
}
