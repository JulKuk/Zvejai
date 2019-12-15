using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Flyweight
{
    public class RedO : FlyWeightObsticale
    {
        public RedO()
        {
            Health_points = 20;
            Type = "R";
            PosX = 0;
            PosY = 0;
        }

        public override void Display(int total)
        {
            Console.WriteLine("Kiekis: " + total + " tipas " + Type);
        }
    }
}
