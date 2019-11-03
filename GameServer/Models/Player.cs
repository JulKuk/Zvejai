using GameServer.Models.Strategy;
using System;
using GameServer.Models.Observer;
using System.Collections.Generic;
namespace GameServer.Models
{
    public class Player :Entity
    {
        public string Name { get; set; }
        public long PosX { get; set; }
        public long PosY { get; set; }
        public int health_points { get; set; }
        public Weapon granade { get; set; }
        public Weapon pistol { get; set; }

        public float speed { get; set; }

        public Istrategy algorithm;

        private List<HealthPointTracker> players = new List<HealthPointTracker>();

       

        public void setStrategy(Istrategy algorithm)
        {
            this.algorithm = algorithm;
        }

        public void Move()
        {
            algorithm.Move(this);
        }

        public override void SayHello()
        {
            Console.WriteLine("Hi Im a player and my name is " + Name + " and I have " + health_points + " HP");
            Console.WriteLine("My coordinates are X" + PosX + " Y: " + PosY);
        }

        public int GetHealthPoint()
        {
            return health_points;
        }

        public Weapon GetGranade()
        {
            return granade;
        }

        public Weapon GetPistol()
        {
            return pistol;
        }

        public void setHealthPoints(int health_points)
        {
            this.health_points = health_points;
        }

        public void setGranade(Weapon granade)
        {
            this.granade = granade;
        }

        public void setPistol(Weapon pistol)
        {
            this.pistol = pistol;
        }

        public string GetPlayerName()
        {
            return Name;
        }

        public void UpdateHealth(int hp)
        {
            health_points += hp;
        }

        public void Notify(int hp)
        {
            foreach (HealthPointTracker hpTracker in players)
            {
                hpTracker.Update(this, hp);
            }
        }
        
    }
}
