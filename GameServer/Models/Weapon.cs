using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models.Decorator;

namespace GameServer.Models
{
    public class Weapon : Entity
    {
        public long PlayerID { get; set; }
        public float cost { get; set; }
        public int damage { get; set; }
        public int ammo { get; set; }

        public int _kiekKulkuYra { get; set; }

        public override string SayHello()
        {
           return "Im Weapon Entity";
        }

        public override Entity Clone()
        {
            return this.MemberwiseClone() as Weapon;
        }

        public void Reload()
        {
            _kiekKulkuYra = ammo;
        }

    }
}
