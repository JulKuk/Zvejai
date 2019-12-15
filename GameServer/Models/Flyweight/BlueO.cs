using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Flyweight
{
    public class BlueO : FlyWeightObsticale
    {
        public BlueO()
        {
            Health_points = 50;
            Type = "B";
            PosX = 0;
            PosY = 0;
        }

        public override void Display(int total)
        {
            Console.WriteLine("Kiekis: " + total + " tipas " + Type);
        }
    }
}
