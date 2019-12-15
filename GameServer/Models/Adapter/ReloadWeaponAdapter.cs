using GameServer.Models.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Adapter
{
    public class ReloadWeaponAdapter: IPlayerState
    {

        public void changePlayerState(Player player)
        {
            player.Weapon._kiekKulkuYra = player.Weapon.ammo;
        }
    }
}
