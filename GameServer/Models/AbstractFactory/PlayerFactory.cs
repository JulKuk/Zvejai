using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.AbstractFactory
{
    public class PlayerFactory : AbstractFactory
    {
        public override Obsticale CreateObsticale(string Type)
        {
            throw new NotImplementedException();
        }

        public override Player CreatePlayer()
        {
            return new Player { Name = "Julius", health_points = 100, PosX = 20, PosY = 50, };
        }

        public override Weapon CreateWeapon(string GunType)
        {
            throw new NotImplementedException();
        }
    }
}
