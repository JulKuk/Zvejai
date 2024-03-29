﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models.Facade
{
    public class GameFacade
    {
        private WebClient WebClient = new WebClient();
        private Game Game = new Game();

        public async Task<ICollection<Player>> GetAllPlayersFromDatabase()
        {
            return await WebClient.GetAllPlayerAsync();
        }

        public async Task<ICollection<Bullet>> GetAllBulletsFromDatabase()
        {
            return await WebClient.GetAllBulletsAsync();
        }

        public async Task<Uri> AddPlayerToDatabase(Player player)
        {
            return await WebClient.CreatePlayerAsync(player);
        }

        public async Task<Uri> AddBulletToDatabase(Bullet bullet)
        {
            return await WebClient.CreateBulletAsync(bullet);
        }

        public async Task<Uri> UpdatePlayerToDatabase(Player player)
        {
            return await WebClient.UpdatePlayerAsync(player);
        }

        public async Task<Uri> UpdateBulletToDatabase(Bullet bullet)
        {
            return await WebClient.UpdateBulletAsync(bullet);
        }

        public async Task<ICollection<Obsticale>> GetAllObstaclesFromDatabase()
        {
            return await WebClient.GetAllObstaclesAsync();
        }
        public async Task<Uri> addObstacleToDatabase(Obsticale obstacle)
        {
            return await WebClient.CreateObstacleAsync(obstacle);
        }

        public async Task<Player> GetPlayerByID(long id)
        {
            return await WebClient.GetPlayerAsync(id);
        }

        public Game GetGame()
        {
            return Game;
        }

        public async void ConnectToGame()
        {
            Game.Players = await this.GetAllPlayersFromDatabase();
            if(Game.Players?.Count == 0)
            {
                await this.AddPlayerToDatabase(Game.CreatePlayer(1));
                Game._currentPlayerId = 1;
                Game.P1Connected = true;
            }
            else if(Game.Players?.Count == 1)
            {
                await this.AddPlayerToDatabase(Game.CreatePlayer(2));
                Game._currentPlayerId = 2;
                Game.P2Connected = true;
            }
        }


    }
}
