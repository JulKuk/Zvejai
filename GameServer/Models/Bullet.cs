using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Bullet
    {
        public enum Direction
        {
            Left, Right, Up, Down
        }
        public int bulletID { get; set; }

        public long posX { get; set; }

        public long posY { get; set; }

        public float speed { get; set; }

        public bool visible { get; set; }

        public long PlayerID { get; set; }

        public Direction shootingDir { get; set; }

        public void Move()
        {
            switch (shootingDir)
            {
                case Direction.Right:
                    posX += Convert.ToInt64(speed);
                    break;
                case Direction.Left:
                    posX -= Convert.ToInt64(speed);
                    break;
                case Direction.Up:
                    posY -= Convert.ToInt64(speed);
                    break;
                case Direction.Down:
                    posY += Convert.ToInt64(speed);
                    break;
            }
        }

        


    }
}
