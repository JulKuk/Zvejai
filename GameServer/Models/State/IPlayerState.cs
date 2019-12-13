using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.State
{
    public interface IPlayerState
    {
        void changePlayerState(Player player);

    }
}
