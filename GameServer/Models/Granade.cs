using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Granade : Weapon
    {
        public string name { get; set; }
        public override void SayHello()
        {
            Console.WriteLine("Im Granade My name is " + name + " my Cost : " + cost + " $ My damage is " + damage + " and I have " + ammo + " ammo.");
        }
    }
}
