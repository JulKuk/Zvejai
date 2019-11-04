using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Observer
{
    public class CHP : HealthPoints
    {
        private List<Player> players;
        private List<HealthPoints> health;

        public CHP()
        {
            players = new List<Player>();
            health = new List<HealthPoints>();
        }

        public Player CheckHealth
        { 
            get { return null; }
            set 
            {
                bool hasElement = false;
                foreach (Player p in players)
                {
                    if (p.id == value.id)
                    {
                        hasElement = true;
                        break;
                    }
                }

                if (!hasElement)
                {
                    players.Add(value);
                    health.Add(new HealthPoints(value.health_points, value.id));
                }
                
                foreach (Player p in players)
                {
                    if (p.id == value.id)
                    {
                        //int hp = 0;
                        foreach (HealthPoints item in health)
                        {
                            if (item.GetID() == value.id)
                            {
                                if (item.GetHP() != value.health_points)
                                {
                                    item.SetHP(value.health_points);
                                    p.health_points = value.health_points;
                                    Notify();
                                }
                            }
                        }
                        
                    }
                }
            }
        }
    }
}
