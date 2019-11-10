using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.AbstractFactory
{
    public abstract class AbstractFactory
    {
        public abstract Player CreatePlayer();

        public abstract Weapon CreateWeapon(String GunType);

        public abstract Obsticale CreateObsticale(String Type);

        public abstract Player GetPlayer();

        public abstract Obsticale Clone();
    }
}
