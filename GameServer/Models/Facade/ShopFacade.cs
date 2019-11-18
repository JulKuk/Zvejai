using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models.Decorator;

namespace GameServer.Models.Facade
{

    public enum weaponNames
    { Bazuka, Automatas, Pistoletas, Sniperis }

    public class ShopFacade
    {

        ShopManager shopManager = new ShopManager();

        Item bazuka = new Item(weaponNames.Bazuka, 200);
        Item automat = new Item(weaponNames.Automatas, 100);
        Item pistol = new Item(weaponNames.Pistoletas, 50);
        Item sniper = new Item(weaponNames.Sniperis, 300);

        public Ginklas Open(Player p, weaponNames name)
        {
            shopManager.shopInventory.AddItem(bazuka);
            shopManager.shopInventory.AddItem(automat);
            shopManager.shopInventory.AddItem(pistol);
            shopManager.shopInventory.AddItem(sniper);
            shopManager.setPlayerGold(p);

            Item wantedItem;

            switch (name)
            {
                case weaponNames.Automatas:
                    wantedItem = automat;
                    break;
                case weaponNames.Pistoletas:
                    wantedItem = pistol;
                    break;
                case weaponNames.Bazuka:
                    wantedItem = bazuka;
                    break;
                case weaponNames.Sniperis:
                    wantedItem = sniper;
                    break;
                default:
                    wantedItem = pistol;
                    break;
            }

            return shopManager.SetPlayerWeapon(p, wantedItem);
            

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

        public Ginklas SetPlayerWeapon(Player p, Item item)
        {
            switch (item.name)
            {
                case weaponNames.Bazuka:
                    if (CanBuyItem(p, item))
                    {
                        return new Bazuka(p);
                    }
                    break;
                case weaponNames.Automatas:
                    if (CanBuyItem(p, item))
                    {
                        return new Automatas(p);
                    }
                    break;
                case weaponNames.Pistoletas:
                    if (CanBuyItem(p, item))
                    {
                        return new Pistoletas(p);
                    }
                    break;
                case weaponNames.Sniperis:
                    if (CanBuyItem(p, item))
                    {
                        return new Snaiperis(p);
                    }
                    break;
            }
            return new Pistoletas(p);
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
            if (playerGold >= item.price)
            {
                return true;
            }
            return false;
        }
    }

    public class Item
    {
        public weaponNames name;
        public int price;

        Weapon b = new Bazooka();

        public Item(weaponNames name, int price)
        {
            this.name = name;
            this.price = price;
        }
    }
}
