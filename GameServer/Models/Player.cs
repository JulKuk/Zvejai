using GameServer.Models.Strategy;
using System;
namespace GameServer.Models
{
    public class Player :Entity
    {
        public string Name { get; set; }
        public long PosX { get; set; }
        public long PosY { get; set; }
        public int health_points { get; set; }

        public float speed { get; set; }

        public Istrategy algorithm;

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
    }
}
