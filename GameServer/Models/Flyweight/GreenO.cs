using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Flyweight
{
    public class GreenO : FlyWeightObsticale
    {
        public GreenO()
        {
            Health_points = 30;
            Type = "G";
            PosX = 0;
            PosY = 0;
        }

        public override void Display(int total)
        {
            Console.WriteLine("Kiekis: " + total + " tipas " + Type);
        }
    }
}
