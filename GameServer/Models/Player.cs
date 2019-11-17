﻿using GameServer.Models.Strategy;
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
        public int points { get; set; }

        private List<Weapon> playerGuns = new List<Weapon>();

        public Weapon defaultGun { get; set; }

        public float speed { get; set; }

        public Istrategy algorithm;
       

        public void setStrategy(Istrategy algorithm)
        {
            this.algorithm = algorithm;
        }

        public void Move()
        {
            algorithm.action(this);
        }

        public override string SayHello()
        {
            return this.Name;
        }

        public void UpdateHealth(int hp)
        {
            health_points += hp;
            Update();
        }

        //public void Notify(int hp)
        //{
        //    foreach (HealthPointTracker hpTracker in players)
        //    {
        //        hpTracker.Update(this, hp);
        //    }
        //}

        public override void Update()
        {
            Console.WriteLine(this.Name + " health is: " + this.health_points);
        }

        public void addGuns(Weapon weapon)
        {
            playerGuns.Add(weapon);
            defaultGun = weapon;
        }

        public List<Weapon> getPlayerGuns()
        {
            return playerGuns;
        }
        
    }
}
