using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GameServer.Models.State;

namespace GameServer
{
    public static class Game
    {
        private static bool _isStarted;
        private static PlayerState PlayerState = new PlayerState();

        public static IServiceProvider ServiceProvider { get; set; }

        public static void StartGame()
        {
            _isStarted = true;
            var timer = new Timer();
            timer.Elapsed += timer1_Tick;
            timer.Interval = 200;
            timer.Start();
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        private static async void timer1_Tick(object sender, EventArgs e)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<PlayerContext>();
                var bullets = db.Bullet.ToList();
                var Players = db.Players.Include(player => player.Weapon).Include(player => player.Weapons).ToList();
                if (bullets.Count != 0)
                {
                    foreach (var bullet in bullets)
                    {
                        if (bullet.visible)
                        {
                            if (bullet.PlayerID == 1)
                            {
                                Player PlayerToHit = Players[1];
                                Player CurrectPlayer = Players[0];
                                if (bullet.posY > 5 && bullet.posY < 310 && bullet.posX > 5 && bullet.posX < 310)
                                {
                                    if (PlayerToHit != null && PlayerToHit.PosX <= bullet.posX + 10 && PlayerToHit.PosY <= bullet.posY +10 )
                                    {
                                        bullet.visible = false;
                                        PlayerToHit.reduceHealth(CurrectPlayer.Weapon.damage);
                                        CurrectPlayer.points += 50;
                                        PlayerState.ChangeState(CurrectPlayer);
                                        db.Players.Update(PlayerToHit);
                                        db.Players.Update(CurrectPlayer);
                                        db.Bullet.Update(bullet);
                                    }
                                    else
                                    {
                                        bullet.Move();
                                        db.Bullet.Update(bullet);
                                    }

                                }
                                else
                                {
                                    bullet.visible = false;
                                    db.Bullet.Update(bullet);
                                }
                            }
                            else
                            {
                                Player PlayerToHit = Players[0];
                                Player CurrectPlayer = Players[1];
                                if (bullet.posY > 5 && bullet.posY < 310 && bullet.posX > 5 && bullet.posX < 310)
                                {
                                    if (PlayerToHit != null && PlayerToHit.PosX == bullet.posX && PlayerToHit.PosY == bullet.posY)
                                    {
                                        bullet.visible = false;
                                        PlayerToHit.reduceHealth(CurrectPlayer.Weapon.damage);
                                        CurrectPlayer.points += 50;
                                        PlayerState.ChangeState(CurrectPlayer);
                                        db.Players.Update(PlayerToHit);
                                        db.Players.Update(CurrectPlayer);
                                        db.Bullet.Update(bullet);
                                    }
                                    else
                                    {
                                        bullet.Move();
                                        db.Bullet.Update(bullet);
                                    }

                                }
                                else
                                {
                                    bullet.visible = false;
                                    db.Bullet.Update(bullet);
                                }
                            }
                        }
                    }
                    db.SaveChanges();
                }
                
                db.Dispose();
            }
        }

        internal static bool IsStarted()
        {
            return _isStarted;
        }
    }
}
