using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Command
{
    public interface ICommand
    {
        void Execute();

        void undo(long diff);
    }
}
