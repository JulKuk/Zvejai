using GameServer.Models;
using GameServer.Models.AbstractFactory;
using GameServer.Models.Command;
using GameServer.Models.Facade;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {
        //Kintamieji
        private Player Player;
        private Weapon Weapon;
        private PlayerContext Database;
        private List<ICommand> Command;
        private ObsticaleFacotry ObsFactory;
        private WeaponsFacotry WeaponsFacotry;
        private ShopFacade Shop;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PlayerContext>().UseInMemoryDatabase(databaseName: "PlayerList").Options;
            Database = new PlayerContext(options);
            Command = new List<ICommand>();
            Player = new Player { health_points = 100, Name = "Julius", speed = 10,PosX=100,PosY=100};
            Weapon = new Weapon { PlayerID = 0, cost = 100, damage=75 };
            Command.Add(new UpCommand(Player));
            Command.Add(new DownCommand(Player));
            Command.Add(new LeftCommand(Player));
            Command.Add(new RightCommand(Player));
            ObsFactory = new ObsticaleFacotry();
            WeaponsFacotry = new WeaponsFacotry();
            Shop = new ShopFacade();


        }


        [Test]
        [Category("Player Tests")]
        [TestCase(10)]
        [TestCase(20)]
        [TestCase(40)]
        public void Update_Player_Hp(int hp)
        {
            var temphp = Player.health_points;
            Player.UpdateHealth(hp);
            Assert.AreEqual(temphp + hp, Player.health_points);
        }

        [Test]
        [Category("Player Tests")]
        public void Add_Gun_to_Player()
        {
            Player.addGuns(Weapon);
            Assert.AreEqual(1, Player.Weapons.Count);
        }

        [Test]
        [Category("Player Tests")]
        public void Get_Player_Guns()
        {
            List<Weapon> Guns = Player.getPlayerGuns();
            Assert.AreEqual(Player.Weapons, Guns);
        }
        [Test]
        [Category("Player Tests")]
        public void Clone_player()
        {
            Player clone = Player.Clone() as Player;
            Assert.AreEqual(Player.Name, clone.Name);
            Assert.AreEqual(Player.health_points, clone.health_points);
        }
        [Test]
        [Category("Player Tests")]
        [TestCase(1000,"P")]
        [TestCase(500, "G")]
        [TestCase(1500, "S")]
        public void Player_Can_buy_Gun(int money,string gunType)
        {
            Player.points = money;
            Shop.Open(Player);
            Assert.IsTrue(Shop.shopManager.CanBuyItem(Player, gunType));
        }
        [Test]
        [Category("Player Tests")]
        [TestCase(1, "B")]
        [TestCase(2, "S")]
        [TestCase(3, "P")]
        public void Player_Cant_buy_Gun(int money, string gunType)
        {
            Player.points = money;
            Shop.Open(Player);
            Assert.IsFalse(Shop.shopManager.CanBuyItem(Player, gunType));
        }


        [Test]
        [Category("Player Tests")]
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(20)]
        public void reduce_health(int health)
        {
            var hp = Player.health_points - health;
            Player.reduceHealth(health);
            Assert.AreEqual(hp, Player.health_points);

        }


        [Test]
        [Category("Command Tests")]
        [TestCase(10)]
        [TestCase(20)]
        [TestCase(50)]
        public void Player_Up_Command(int speed)
        {
            var nextPoss = Player.PosY - speed;
            Player.speed = speed;
            Command[0].Execute();
            Assert.AreEqual(nextPoss, Player.PosY);
        }

        [Test]
        [Category("Command Tests")]
        [TestCase(10)]
        [TestCase(20)]
        [TestCase(50)]
        public void Player_Down_Command(int speed)
        {
            var nextPoss = Player.PosY + speed;
            Player.speed = speed;
            Command[1].Execute();
            Assert.AreEqual(nextPoss, Player.PosY);
        }

        [Test]
        [Category("Command Tests")]
        [TestCase(5)]
        [TestCase(15)]
        [TestCase(30)]
        public void Player_left_Command(int speed)
        {
            var nextPoss = Player.PosX - speed;
            Player.speed = speed;
            Command[2].Execute();
            Assert.AreEqual(nextPoss, Player.PosX);
        }

        [Test]
        [Category("Command Tests")]
        [TestCase(5)]
        [TestCase(15)]
        [TestCase(30)]
        public void Player_Right_Command(int speed)
        {
            var nextPoss = Player.PosX + speed;
            Player.speed = speed;
            Command[3].Execute();
            Assert.AreEqual(nextPoss, Player.PosX);
        }

        [Test]
        [Category("Abstract Factory Tests")]
        [TestCase("R")]
        [TestCase("G")]
        [TestCase("B")]
        public void Create_Obsticales(string type)
        {
           if(type == "R")
            {
                Obsticale temp = ObsFactory.CreateObsticale(type);
                Assert.AreEqual("Red", temp.Type);
                Assert.AreEqual(20,temp.Health_points);
            }
           else if(type == "G")
            {
                Obsticale temp = ObsFactory.CreateObsticale(type);
                Assert.AreEqual("Green", temp.Type);
                Assert.AreEqual(30, temp.Health_points);
            }
           else 
            {
                Obsticale temp = ObsFactory.CreateObsticale(type);
                Assert.AreEqual("Blue", temp.Type);
                Assert.AreEqual(50, temp.Health_points);
            }
        }
        [Test]
        [Category("Abstract Factory Tests")]
        [TestCase("S")]
        [TestCase("A")]
        [TestCase("P")]
        [TestCase("B")]
        [TestCase("G")]
        public void Create_Guns(string type)
        {
            if (type == "S")
            {
                Sniper temp = WeaponsFacotry.CreateWeapon(type) as Sniper;
                Assert.AreEqual(10, temp.ammo);
                Assert.AreEqual("AWP", temp.name);
                Assert.AreEqual(75, temp.damage);
                Assert.AreEqual(900, temp.cost);
            }
            else if (type == "A")
            {
                Automat temp = WeaponsFacotry.CreateWeapon(type) as Automat;
                Assert.AreEqual(30, temp.ammo);
                Assert.AreEqual("AK-47", temp.name);
                Assert.AreEqual(45, temp.damage);
                Assert.AreEqual(750, temp.cost);
            }
            else if (type == "P")
            {
                Pistol temp = WeaponsFacotry.CreateWeapon(type) as Pistol;
                Assert.AreEqual(7, temp.ammo);
                Assert.AreEqual("Desert Eagle", temp.name);
                Assert.AreEqual(30, temp.damage);
                Assert.AreEqual(500, temp.cost);
            }
            else if (type == "B")
            {
                Bazooka temp = WeaponsFacotry.CreateWeapon(type) as Bazooka;
                Assert.AreEqual(1, temp.ammo);
                Assert.AreEqual("RukyBazuky", temp.name);
                Assert.AreEqual(100, temp.damage);
                Assert.AreEqual(1000, temp.cost);
            }
            else
            {
                Granade temp = WeaponsFacotry.CreateWeapon(type) as Granade;
                Assert.AreEqual(1, temp.ammo);
                Assert.AreEqual("small", temp.name);
                Assert.AreEqual(10, temp.damage);
                Assert.AreEqual(0, temp.cost);
            }
        }


    }
}