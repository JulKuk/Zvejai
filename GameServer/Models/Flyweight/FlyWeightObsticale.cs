using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Flyweight
{
    public abstract class FlyWeightObsticale
    {        
            public int Health_points { get; set; }
            public string Type { get; set; }
            public long PosX { get; set; }
            public long PosY { get; set; }

 
            public abstract void Display(int total);
    }
}

