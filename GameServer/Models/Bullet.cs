using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Bullet
    {
        public int bulletID { get; set; }

        public long posX { get; set; }

        public long posY { get; set; }

        public float speed { get; set; }

        public bool visible { get; set; }


    }
}
