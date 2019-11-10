using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.AbstractFactory
{
    public class ObsticaleFacotry : AbstractFactory
    {
        public override Obsticale CreateObsticale(string Type)
        {
            if(Type.Equals("R"))
            {
                return new Obsticale { Type = "Red", Health_points = 20 };
            }
            if (Type.Equals("G"))
            {
                return new Obsticale { Type = "Green", Health_points = 30 };
            }

            return new Obsticale { Type = "Blue", Health_points = 50 };
        }

        public override Player CreatePlayer()
        {
            throw new NotImplementedException();
        }

        public override Weapon CreateWeapon(string GunType)
        {
            throw new NotImplementedException();
        }

        public override Player GetPlayer()
        {
            throw new NotImplementedException();
        }

        public override Obsticale Clone()
        {
           // Obsticale naujas = this.MemberwiseClone() as Obsticale;

            return this.MemberwiseClone() as Obsticale;
        }
    }
}
