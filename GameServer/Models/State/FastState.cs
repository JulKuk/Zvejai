using GameServer.Models.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class FastState : IPlayerState
    {
        PlayerState playerState;

        public FastState(PlayerState state)
        {
            playerState = state;
        }
        public void changePlayerState(Player player)
        {
            if (player.points >= 100 && player.points < 300)
            {
                player.speed = 20.0f;
            }
            else
            {
                playerState.setCurrentState(playerState.getInfAmmoState());
            }
        }
    }
}
