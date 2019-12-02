using Microsoft.EntityFrameworkCore;
using System;
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

    }
}

