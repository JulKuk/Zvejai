using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Decorator
{
    public class Gameboard
    {
        private Ginklas ginklai;

        private int gameboardX = 320;
        private int gameboardY = 320;
        public int step = 10;

        public Ginklas getGinklai()
        {
            return ginklai;
        }

        public void setGinklai(Ginklas ginklai)
        {
            this.ginklai = ginklai;
        }

        public String showAllGinklai()
        {
            return ginklai.Show();
        }

    }
}