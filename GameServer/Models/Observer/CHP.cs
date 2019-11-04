using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Observer
{
    public class CHP : HealthPoints
    {
        private List<Player> players;
        private List<Tuple<long, int>> Health;

        public CHP()
        {
            players = new List<Player>();
            Health = new List<Tuple<long, int>>();
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
                    Health.Add(Tuple.Create(value.id, value.health_points));
                }
                
                foreach (Player p in players)
                {
                    if (p.id == value.id)
                    {
                        int hp = 0;
                        foreach (Tuple<long, int> tuple in Health)
                        {
                            if (tuple.Item1 == value.id)
                            {
                                hp = tuple.Item2;
                            }
                        }
                        if (hp != value.health_points)
                        {
                            p.health_points = value.health_points;
                            Notify();
                        }
                    }
                }
            }
        }
    }
}
