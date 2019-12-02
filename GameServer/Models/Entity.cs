using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models.Observer;

namespace GameServer.Models
{
    public abstract class Entity: IObserver
    {
        public long id { get; set; }

        public abstract string SayHello();

        public virtual void Update()
        {
        
        }

        public abstract Entity Clone();
    }
}
