using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Facade
{
    public class ShopFacade
    {
        //cia susideti tuos testus kur destytojui rodem

        public ShopManager shopManager = new ShopManager();

        Item bazuka = new Item("Bazuka", new Bazooka());
        Item automat = new Item("Automatas", new Automat());
        Item pistol = new Item("Pistoletas", new Pistol());
        Item sniper = new Item("Sniperis", new Sniper());

        public void Open(Player p)
        {
            shopManager.shopInventory.AddItem(bazuka);
            shopManager.shopInventory.AddItem(automat);
            shopManager.shopInventory.AddItem(pistol);
            shopManager.shopInventory.AddItem(sniper);
            shopManager.setPlayerGold(p);

            //bool canSellPistol = shopManager.CanBuyItem(p, pistol);

            //bool canSellAutomat = shopManager.CanBuyItem(p, automat);
        }
    }

    public class ShopManager
    {
        public ShopInventory shopInventory = new ShopInventory();
        public PlayerInventory playerInventory;

        public bool CanBuyItem(Player p, Item i)
        {
            if (!playerInventory.PlayerHasEnoughMoney(i) || !shopInventory.ShopHasItem(i))
            {
                return false;
            }
            return true;
        }

        public void setPlayerGold(Player p)
        {
            this.playerInventory = new PlayerInventory(p.points);
        }

        public bool CanBuyItem(Player player, string gunType)
        {
            if (player.points > 100)
            {
                return true;
            }
            else return false;
        }
    }

    public class ShopInventory
    {
        public List<Item> shopInventory;

        public ShopInventory()
        {
            this.shopInventory = new List<Item>();
        }

        public void AddItem(Item item)
        {
            shopInventory.Add(item);
        }

        public bool ShopHasItem(Item item)
        {
            if (shopInventory.Contains(item))
            {
                return true;
            }
            return false;
        }
    }

    public class PlayerInventory
    {
        public int playerGold;

        public PlayerInventory(int gold)
        {
            this.playerGold = gold;
        }

        public bool PlayerHasEnoughMoney(Item item)
        {
            if (playerGold >= item.weapon.cost)
            {
                return true;
            }
            return false;
        }
    }

    public class Item : Weapon
    {
        public string name;
        public Weapon weapon;

        Weapon b = new Bazooka();

        public Item(string name, Weapon weapon)
        {
            this.name = name;
            this.weapon = weapon;
        }
    }
}
