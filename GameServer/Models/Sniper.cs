using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Sniper : Weapon
    {
        public string name { get; set; }
        public override string SayHello()
        {
            return name = "Snaiperis";
        }
    }
}
