using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public abstract class Weapon : Entity
    {
        
        public float cost { get; set; }
        public int damage { get; set; }
        public int ammo { get; set; }

        public override void SayHello()
        {
            Console.WriteLine("Im Weapon Entity");
        }
    }
}
