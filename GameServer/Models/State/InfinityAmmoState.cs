using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.State
{
    public class InfinityAmmoState : IPlayerState
    {
        PlayerState playerState;

        public InfinityAmmoState(PlayerState state)
        {
            playerState = state;
        }
        public void changePlayerState(Player player)
        {
            if (player.points >= 300 && player.points <= 1000)
            {
                player.speed = 10.0f;
                player.Weapon._kiekKulkuYra = 999;
            }
            else
            {
                player.Score++;
                player.points = 0;
                playerState.setCurrentState(playerState.getSlowState());
            }
        }
    }
}
