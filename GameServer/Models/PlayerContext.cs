﻿using Microsoft.EntityFrameworkCore;
using System;
using GameServer.Models;
namespace GameServer.Models
{
    public class PlayerContext : DbContext
    {
        public DbSet<Weapon> Weapons
        {
            get;
            set;
        }

        public DbSet<Player> Players { 
            get;
            set;  
        }
     
       
        public PlayerContext(DbContextOptions<PlayerContext> options)
            : base(options)
        {
            
        }
     
       
        public DbSet<Obsticale> Obsticale { get; set; }

    }
}

