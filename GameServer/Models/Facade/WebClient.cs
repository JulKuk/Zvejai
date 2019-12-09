using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models.Facade
{
    public class WebClient
    {
        private HttpClient client = new HttpClient();
        //WeBAPI
        private string path = "https://localhost:44371/";

        public async Task<Uri> CreatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PostAsync("https://localhost:44371/api/player", new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            await response.Content.ReadAsStringAsync();


            // return URI of the created resource.
            return response.Headers.Location;
        }
        public async Task<Uri> CreateBulletAsync(Bullet bullet)
        {
            HttpResponseMessage response = await client.PostAsync("https://localhost:44371/api/Bullets", new StringContent(JsonConvert.SerializeObject(bullet), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            await response.Content.ReadAsStringAsync();


            // return URI of the created resource.
            return response.Headers.Location;
        }

        public async Task<Uri> UpdatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PutAsync(path + "api/player/" + player.id, new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            await response.Content.ReadAsStringAsync();


            // return URI of the created resource.
            return response.Headers.Location;
        }
        public async Task<Uri> UpdateBulletAsync(Bullet bullet)
        {
            HttpResponseMessage response = await client.PutAsync(path + "api/Bullets/" + bullet.bulletID, new StringContent(JsonConvert.SerializeObject(bullet), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync();


            // return URI of the created resource.
            return response.Headers.Location;
        }
        public async Task<ICollection<Obsticale>> GetAllObstaclesAsync()
        {
            ICollection<Obsticale> obstacle = null;
            HttpResponseMessage response = await client.GetAsync(path + "api/obstacles");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                obstacle = JsonConvert.DeserializeObject<ICollection<Obsticale>>(content);
            }
            return obstacle;
        }

        public async Task<Uri> CreateObstacleAsync(Obsticale obstacle)
        {
            HttpResponseMessage response = await client.PostAsync("https://localhost:44371/api/obstacles", new StringContent(JsonConvert.SerializeObject(obstacle), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            await response.Content.ReadAsStringAsync();


            // return URI of the created resource.
            return response.Headers.Location;
        }

        public async Task<ICollection<Player>> GetAllPlayerAsync()
        {
            ICollection<Player> players = null;
            HttpResponseMessage response = await client.GetAsync(path + "api/player");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                players = JsonConvert.DeserializeObject<ICollection<Player>>(content);
            }
            return players;
        }

        public async Task<ICollection<Bullet>> GetAllBulletsAsync()
        {
            ICollection<Bullet> bullets = null;
            HttpResponseMessage response = await client.GetAsync(path + "api/Bullets");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                bullets = JsonConvert.DeserializeObject<ICollection<Bullet>>(content);
            }
            return bullets;
        }

        public async Task<Player> GetPlayerAsync(long id)
        {
            Player players = null;
            HttpResponseMessage response = await client.GetAsync(path + "api/player/" + id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                players = JsonConvert.DeserializeObject<Player>(content);
            }
            return players;
        }
    }
}
