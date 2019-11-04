using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Observer
{
    public interface IObserver // : HealthPointTracker
    {
        //public void Update(Player players, int hp)
        //{
        //    if (Math.Abs(hp) > 0)
        //    {
        //        players.UpdateHealth(hp);
        //    }
        //}

        void Update();
    }
}
