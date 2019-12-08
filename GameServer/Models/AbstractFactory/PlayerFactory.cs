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
            //return new Player { Name = player.Name, health_points = player.health_points, PosX = player.PosX, PosY = player.PosY, points = player.points, algorithm = player.algorithm };
            return null;
        }

        public override Weapon CreateWeapon(string GunType)
        {
            throw new NotImplementedException();
        }

        public override Player GetPlayer()
        {
            PlayerBuilder player = new PlayerBuilder();
            return player.startPlayer().AddGranade().AddHealthPoints().AddPistol().build();
        }

       // public override Obsticale Clone()
       // {
       //     throw new NotImplementedException();
       // }
    }
}
