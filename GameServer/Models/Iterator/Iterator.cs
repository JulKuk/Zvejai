using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Iterator
{
    public abstract class Iterator
    {
        public abstract object First();
        public abstract object Next(); 
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }
}
