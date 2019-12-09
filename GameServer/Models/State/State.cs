using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class State
    {
        public int StateID { get; set; }
        public enum PlayerState
        {
            invisible,Fast,Slow
        }

        private PlayerState state;

        public void goNext(Player player)
        {
            switch(player.points)
            {
                case 0:
                    state = PlayerState.Slow;
                    player.speed = 5;
                    break;
                case 1000:
                    state = PlayerState.Fast;
                    player.speed = 25;
                    break;
                case 1500:
                    state = PlayerState.invisible;
                    player.speed = 10;
                    break;

            }
        }

        public PlayerState getState()
        {
            return state;
        }


    }
}
