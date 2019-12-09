using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Iterator
{
    public class ConcreteAggregator : Iterator
    {
        private Weapons2 _aggregate;
        private int _current = 0;

        public ConcreteAggregator(Weapons2 aggregate) 
        {
            this._aggregate = aggregate; 
        }
        public override object First()
        { 
            return _aggregate[0]; 
        }
        public override object Next() 
        { 
            object ret = null; 
            if (_current<_aggregate.Count - 1) 
            { 
                ret = _aggregate[++_current]; 
            } 
            return ret; 
        }
        public override object CurrentItem() 
        { 
            return _aggregate[_current]; 
        } 
        // Gets whether iterations are complete 
        public override bool IsDone() 
        { 
            return _current >= _aggregate.Count; 
        }

    }
}
