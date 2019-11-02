using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.AbstractFactory
{
    public abstract class AbstractFactory
    {
        public abstract Entity CreatePlayer();

        public abstract Entity CreateWeapon(String GunType);

        public abstract Entity CreateObsticale(String Type);

    }
}
