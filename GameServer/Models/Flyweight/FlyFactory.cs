using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models.Iterator;

namespace GameServer.Models.Flyweight
{
    public class FlyFactory : Aggregate
    {
        private Dictionary<char, FlyWeightObsticale> _sliders =
        new Dictionary<char, FlyWeightObsticale>();
        //private ArrayList _items = new ArrayList();
        private List<FlyWeightObsticale> _items = new List<FlyWeightObsticale>();

        public FlyWeightObsticale GetObs(char key)
        {
            FlyWeightObsticale slider = null;
            if (_sliders.ContainsKey(key))
            {
                slider = _sliders[key];
            }
            else
            {
                switch (key)
                {
                    case 'B': slider = new BlueO(); break;
                    case 'G': slider = new GreenO(); break;
                    case 'R': slider = new RedO(); break;
                }
                _sliders.Add(key, slider);
            }
            return slider;
        }

        public override Iterator.Iterator CreateIterator()
        {
            return new ConcreteAggregator2(this);
        }

        //public override Iterator CreateIterator()
        // {
        //     return new ConcreteAggregator2(this);
        //  }
        // Gets item count 
        public int Count
        {
            get { return _items.Count; }
        }
        public FlyWeightObsticale this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }
    }
}
