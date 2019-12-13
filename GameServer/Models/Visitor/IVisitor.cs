using GameServer.Models.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Visitor
{
    public interface IVisitor
    {
        void visit(RightCommand right);
        void visit(UpCommand up);
        void visit(DownCommand down);
        void visit(LeftCommand left);

    }
}
