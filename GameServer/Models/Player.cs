using GameServer.Models.Strategy;
using System;
using GameServer.Models.Observer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using GameServer.Models.Iterator;

namespace GameServer.Models
{
    public class Player : Entity
    {
        public string Name { get; set; }
        public long PosX { get; set; }
        public long PosY { get; set; }
        public int health_points { get; set; }
        public int points { get; set; }

        public List<Weapon> Weapons { get; set; } = new List<Weapon>();

        public Weapon Weapon { get; set; }

        public float speed { get; set; }

        public Istrategy algorithm;
       

        public void setStrategy(Istrategy algorithm)
        {
            this.algorithm = algorithm;
        }

        public void Move(float speed)
        {
            algorithm.action(this,speed);
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
            Weapons.Add(weapon);
            Weapon = weapon;
        }

        public List<Weapon> getPlayerGuns()
        {
            return Weapons;
        }
        public override Entity Clone()
        {
            return this.MemberwiseClone() as Player;
        }
        public void reduceHealth(int health)
        {
            health_points -= health;
        }

        public Bullet Shoot()
        {
            if (Weapon._kiekKulkuYra > 0)
            {
                Weapon._kiekKulkuYra -= 1;
                return new Bullet { PlayerID = id, posX = PosX,posY = PosY, speed = 10, visible = true };
            }

            return null;
        }
        public void reloadWeapon()
        {
            Weapon._kiekKulkuYra = Weapon.ammo;
        }


    }
}
