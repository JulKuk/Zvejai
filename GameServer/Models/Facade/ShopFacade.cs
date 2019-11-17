using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Facade
{
    public class ShopFacade
    {
        //cia susideti tuos testus kur destytojui rodem

        ShopManager shopManager = new ShopManager();

        Item pistol = new Item("Pistoletas", 20);
        Item automat = new Item("Automatas", 100);

        void Open(Player p)
        {
            shopManager.shopInventory.AddItem(pistol);
            shopManager.shopInventory.AddItem(automat);
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
            if (playerGold >= item.value)
            {
                return true;
            }
            return false;
        }
    }

    public class Item
    {
        public string name;
        public int value;

        public Item(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
