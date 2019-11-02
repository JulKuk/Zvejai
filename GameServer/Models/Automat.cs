using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Automat : Weapon
    {
        public string name { get; set; }
        public override void SayHello()
        {
            Console.WriteLine("Im Automat My name is " + name + " my Cost : " + cost + " $ My damage is " + damage + " and I have " + ammo + " ammo.");
        }
    }
}
