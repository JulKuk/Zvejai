﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Automat : Weapon
    {
        public string name { get; set; }
        public override string SayHello()
        {
            return name = "Automatas";
        }
    }
}
