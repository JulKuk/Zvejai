using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models.Builder;

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
            //return new Player { Name = "Julius", health_points = 100, PosX = 20, PosY = 50, };
            return null;
        }

        public override Weapon CreateWeapon(string GunType)
        {
            throw new NotImplementedException();
        }

        public override Player GetPlayer()
        {
            //PlayerBuilder player = new PlayerBuilder();
            //return player.startPlayer().AddGranade().AddHealthPoints().AddPistol().build();
            ////return new Player();
            return null;
        }

        public override Obsticale Clone()
        {
            throw new NotImplementedException();
        }
    }
}
