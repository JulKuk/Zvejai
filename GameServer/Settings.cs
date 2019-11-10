using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace GameServer
{
    public class Settings
    {
        private static Settings instance = null;

        public int Levels;
        public int PlayerCount;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Settings getInstance()
        {
            if (instance == null)
            {
                instance = new Settings();
            }
            return instance;
        }
    }
}
