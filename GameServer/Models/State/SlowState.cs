using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.State
{
    public class SlowState : IPlayerState
    {
        PlayerState playerState;

        public SlowState(PlayerState state)
        {
            playerState = state;
        }

        public void changePlayerState(Player player)
        {
            if (player.points >= 0 && player.points < 100)
            {
                player.speed = 5.0f;
            }
            else
            {
                playerState.setCurrentState(playerState.getFastState());
            }

        }
    }
}
