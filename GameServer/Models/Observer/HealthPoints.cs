using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Observer
{
    public class HealthPoints
    {
        private int health;
        private long id;

        public HealthPoints()
        { }

        public HealthPoints(int health, long id)
        {
            this.health = health;
            this.id = id;
        }

        public int GetHP()
        {
            return this.health;
        }

        public void SetHP(int hp)
        {
            this.health = hp;
        }

        public long GetID()
        {
            return this.id;
        }


        private List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver o)
        {
            observers.Add(o);
        }

        public void Detach(IObserver o)
        {
            observers.Add(o);
        }

        public void Notify()
        {
            foreach (IObserver o in observers)
            {
                Console.WriteLine("player notified about the change");
                o.Update();
            }
        }
    }
}
