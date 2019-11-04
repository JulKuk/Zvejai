using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Observer
{
    public class ConcreteHealthPoints : HealthPoints
    {
        
        //private Player player;
        private int health_points;

        public int HealthPoints
        {
            get { return health_points; }
            set 
            {
                if (health_points != value)
                {
                    health_points = value;
                    Notify();
                }
            }
        }
    }
}
