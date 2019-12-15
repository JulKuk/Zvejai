using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Memento
{
    public class Memento
    {

        public class IntefaceMemento
        {
            int points;

            public IntefaceMemento(int newPoints)
            {
                this.points = newPoints;
            }

            public void getPoints(Originator org)
            {
                org.setPoints(this.points);
            }


            private int returnPoints()
            {
                return points;
            }
        }

        public class Originator
        {
            int points;

            public void restorePoints(IntefaceMemento memento)
            {
                memento.getPoints(this);
            }

            public Originator(int newPoints)
            {
                this.points = newPoints;
            }

            public int getPoints()
            {
                return points;
            }

            public void setPoints(int points)
            {
                this.points = points;
            }

            public IntefaceMemento savePoints()
            {
                return new IntefaceMemento(points);
            }
        }

        public class Caretaker
        {
            List<IntefaceMemento> pointList;

            public Caretaker()
            {
                pointList = new List<IntefaceMemento>();
            }

            public void add(IntefaceMemento m)
            {
                pointList.Add(m);
            }

            public IntefaceMemento get(int index)
            {
                IntefaceMemento restorePoints = pointList.ElementAt(index);
                pointList.Remove(restorePoints);
                return restorePoints;
            }

            public IntefaceMemento undo(int index)
            {
                IntefaceMemento restorePoints = pointList.ElementAt(index);
                return restorePoints;
            }
            public IntefaceMemento redo(int index)
            {
                IntefaceMemento restorePoints = pointList.ElementAt(index);
                pointList.RemoveAt(index);
                return restorePoints;
            }

            public int size()
            {
                return pointList.Count;
            }
        }
    }
}