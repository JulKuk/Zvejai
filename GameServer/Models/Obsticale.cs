using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Obsticale : Entity
    {
        public int Health_points { get; set; }
        public string Type { get; set; }

        public override void SayHello()
        {
            Console.WriteLine("Im Obsticale : " + Type + "My HP: " + Health_points);
        }
    }
}
