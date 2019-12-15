using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Obsticale : Entity
    {
        public int Health_points { get; set; }
        public string Type { get; set; }
        public long PosX { get; set; }
        public long PosY { get; set; }

        //public Object Clone()
       // {
      //      Obsticale naujas = this.MemberwiseClone() as Obsticale;
        //    naujas.Health_points = this.Health_points;
         //   naujas.PosX = this.PosX;
         //   naujas.PosY = this.PosY;
          //  naujas.Type = this.Type;

         //   return naujas as Obsticale;

            // throw new NotImplementedException();
     //   }

        public override string SayHello()
        {
            return Type;
        }

        public override Entity Clone()
        { 
            return this.MemberwiseClone() as Obsticale;
        }

    }
}
