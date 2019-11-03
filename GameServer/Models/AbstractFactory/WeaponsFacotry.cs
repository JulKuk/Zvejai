using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.AbstractFactory
{
    public class WeaponsFacotry : AbstractFactory
    {
        public override Obsticale CreateObsticale(string Type)
        {
            throw new NotImplementedException();
        }

        public override Player CreatePlayer()
        {
            throw new NotImplementedException();
        }

        public override Weapon CreateWeapon(String GunType)
        {
            if(GunType.Equals("S"))
            {
                return new Sniper { ammo = 10, name = "AWP", damage = 75, cost = 900 };
            }
            if (GunType.Equals("A"))
            {
                return new Automat { ammo = 30, name = "AK-47", damage = 45, cost = 750 };
            }
            if (GunType.Equals("P"))
            {
                return new Pistol { ammo = 7, name = "Desert Eagle", damage = 30, cost = 500 };
            }
            if (GunType.Equals("B"))
            {
                return new Bazooka { ammo = 1, cost = 1000, damage = 100, name = " RukyBazuky" };
            }

            return new Granade { ammo = 1, name = "small", damage = 10, cost = 0 };
        }
    }
}
