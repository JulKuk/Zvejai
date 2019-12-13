using GameServer.Models.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Visitor
{
    public class CommandVisitor : IVisitor
    {
        public CommandVisitor()
        {

        }

        public void visit(RightCommand right)
        {
            right.Execute();
        }

        public void visit(UpCommand up)
        {
            up.Execute();
        }

        public void visit(DownCommand down)
        {
            down.Execute();
        }

        public void visit(LeftCommand left)
        {
            left.Execute();
        }
    }
}
