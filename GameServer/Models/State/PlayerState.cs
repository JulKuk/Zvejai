using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.State
{
    public class PlayerState
    {
        [Key]
        public int Id { get; set; }
        public IPlayerState infAmmo { get; set; }
        public IPlayerState fast { get; set; }
        public IPlayerState slow { get; set; }

        IPlayerState currectState;

        public PlayerState()
        {
            infAmmo = new InfinityAmmoState(this);
            fast = new FastState(this);
            slow = new SlowState(this);
            currectState = slow;
        }

        public IPlayerState GetCurrectState()
        {
            return currectState;
        }
        public void setCurrentState(IPlayerState state)
        {
            currectState = state;
        }
        public void ChangeState(Player player)
        {
            currectState.changePlayerState(player);
        }

        public IPlayerState getFastState()
        {
            return fast;
        }

        public IPlayerState getSlowState()
        {
            return slow;
        }
        public IPlayerState getInfAmmoState()
        {
            return infAmmo;
        }



    }
}
