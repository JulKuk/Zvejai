using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public abstract class Entity
    {
        public long id { get; set; }

        public abstract void SayHello();
    }
}
