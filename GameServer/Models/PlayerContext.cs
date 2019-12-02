﻿using Microsoft.EntityFrameworkCore;
using System;
namespace GameServer.Models
{
    public class PlayerContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Weapon> weapons { get; set; }
       
        public PlayerContext(DbContextOptions<PlayerContext> options)
            : base(options)
        {
        }

    }
}

