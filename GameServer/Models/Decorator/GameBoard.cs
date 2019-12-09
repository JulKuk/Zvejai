using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Decorator
{
    public class Gameboard
    {
        private Ginklas ginklai;

#pragma warning disable CS0414 // The field 'Gameboard.gameboardX' is assigned but its value is never used
        private int gameboardX = 320;
#pragma warning restore CS0414 // The field 'Gameboard.gameboardX' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'Gameboard.gameboardY' is assigned but its value is never used
        private int gameboardY = 320;
#pragma warning restore CS0414 // The field 'Gameboard.gameboardY' is assigned but its value is never used
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