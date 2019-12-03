using GameServer.Models.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Builder
{
    public class PlayerConfigurator
    {
        private Player player;

        public PlayerConfigurator (Player player)
        {
            this.player = player;
        }

        public PlayerConfigurator AddHealthPoints()
        {
            this.player.health_points = 200;
            return this;
        }

        public PlayerConfigurator AddGranade()
        {
            WeaponsFacotry naujas;
            naujas = new WeaponsFacotry();
            Weapon Temp = naujas.CreateWeapon("");

            this.player.addGuns(Temp);
            return this;
        }

        public PlayerConfigurator AddPistol()
        {
            WeaponsFacotry naujas;
            naujas = new WeaponsFacotry();
            Weapon Temp = naujas.CreateWeapon("P");

            this.player.addGuns(Temp);
            return this;
        }

        public Player build()
        {
            //Console.WriteLine("Created player: " + player.health_points + " pistolestas " + player.getPlayerGuns()[1].damage);
            return player;
        }
    }
}
