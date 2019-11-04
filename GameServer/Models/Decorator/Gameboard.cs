using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Decorator
{
    public class Gameboard
    {
        private Ginklas ginklai;

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
