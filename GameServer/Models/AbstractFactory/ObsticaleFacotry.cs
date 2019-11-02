using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.AbstractFactory
{
    public class ObsticaleFacotry : AbstractFactory
    {
        public override Entity CreateObsticale(string Type)
        {
            if(Type.Equals("R"))
            {
                return new Obsticale { Type = "Red", Health_points = 50 };
            }
            if (Type.Equals("G"))
            {
                return new Obsticale { Type = "Green", Health_points = 10 };
            }

            return new Obsticale { Type = "Blue", Health_points = 75 };
        }

        public override Entity CreatePlayer()
        {
            throw new NotImplementedException();
        }

        public override Entity CreateWeapon(string GunType)
        {
            throw new NotImplementedException();
        }
    }
}
