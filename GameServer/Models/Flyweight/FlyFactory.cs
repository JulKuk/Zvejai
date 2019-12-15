using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Flyweight
{
    public class FlyFactory
    {
        private Dictionary<char, FlyWeightObsticale> _sliders =
        new Dictionary<char, FlyWeightObsticale>();

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
    }
}
