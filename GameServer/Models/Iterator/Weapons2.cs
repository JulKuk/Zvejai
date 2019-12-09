using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Iterator
{
    public class Weapons2 : Aggregate
    {
        private ArrayList _items = new ArrayList(); 

        public override Iterator CreateIterator() 
        { 
            return new ConcreteAggregator(this); 
        } 
        // Gets item count 
        public int Count 
        { 
            get { return _items.Count; } 
        } 
        // Indexer 
        public object this[int index] 
        { 
            get { return _items[index]; }
            set { _items.Insert(index, value); } 
        }
    }
}
