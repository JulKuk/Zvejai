using System;
namespace GameServer.Models
{
    public class Player :Entity
    {
        public string Name { get; set; }
        public long PosX { get; set; }
        public long PosY { get; set; }
        public int health_points { get; set; }

        public override void SayHello()
        {
            Console.WriteLine("Hi Im a player and my name is " + Name + " and I have " + health_points + " HP");
            Console.WriteLine("My coordinates are X" + PosX + " Y: " +PosY);
        }
    }
}
