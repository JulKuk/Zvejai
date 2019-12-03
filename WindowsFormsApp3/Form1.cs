using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameServer.Models;
using GameServer.Models.AbstractFactory;
using GameServer.Models.Strategy;
using GameServer.Models.Builder;
using GameServer.Models.Observer;
using GameServer.Models.Decorator;
using GameServer.Models.Facade;
using GameServer.Models.Command;
using WindowsFormsApp3;
using Newtonsoft.Json;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //WeBAPI
        public string path = "https://localhost:44371/";
        //FaCtory
        private PlayerFactory PlayerFactory = new PlayerFactory();
        //Http Klientas jsonui siusti
        public HttpClient client = new HttpClient();
        enum Direction
        {
            Left, Right, Up, Down, Stop
        }

        private Direction _playerDirection;
        MoveAlgorithm moveAlgorithm = new MoveAlgorithm();

        public ICollection<Player> Players;

        private Player CurrentPlayer;

        private bool createPlayer = false;
        private bool createdPlayer = false;

        private List<Obsticale> obsticaless = new List<Obsticale>();
        private Obsticale obs;
        private bool obsticalescreate = false;
        private bool obsCr = false;
        // List<int> vienas = new List<int> { 1, 2, 3, 4, 5 };

        private ShopFacade shop = new ShopFacade();

        Gameboard board = new Gameboard();

        private bool playerHit = false;
        private CHP observer = new CHP();

        public Form1(  )
        {
            this.KeyPreview = true;

            //pradeda stovedamas
            _playerDirection = Direction.Stop;

            InitializeComponent();
        }

        async Task<ICollection<Player>> GetAllPlayerAsync(string path)
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

        private async void Form1_Load(object sender, EventArgs e)
        {
            Players = await GetAllPlayerAsync(path);
            Connect();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics canvas = e.Graphics;

            //canvas.FillRectangle(Brushes.Green, 10, 10, 100, 100);
            //e.Graphics.FillRectangle(Brushes.Orange, _x, _y, 20, 20);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        async Task<Uri> CreatePlayerAsync(Player player)
        {
            var s = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:44371/api/player", new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            var player2 = await response.Content.ReadAsStringAsync();


            // return URI of the created resource.
            return response.Headers.Location;
        }
        private async void Connect()
        {
            if(Players.Count == 0)
            {
                Player p = PlayerFactory.GetPlayer();
                var url = await CreatePlayerAsync(p);
                textBox1.AppendText("Player 1 Connected.");
            }
            else if (Players.Count == 1)
            {
                textBox1.AppendText("Player 2 Connected.");
            }

            
            // Deserialize the updated product from the response body.
            

            // return URI of the created resource.
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.N:
            //        textBox1.AppendText("Started new game." + Environment.NewLine);
            //        obsticalescreate = true;
            //        e.Handled = true;
            //        break;
            //    case Keys.W:
            //        textBox1.AppendText("Command Up Executed" + Environment.NewLine);
            //        _playerDirection = Direction.Up;
            //        e.Handled = true;
            //        break;
            //    case Keys.S:
            //        textBox1.AppendText("Command Down Executed" + Environment.NewLine);
            //        _playerDirection = Direction.Down;
            //        e.Handled = true;
            //        break;
            //    case Keys.A:
            //        textBox1.AppendText("Command Left Executed" + Environment.NewLine);
            //        _playerDirection = Direction.Left;
            //        e.Handled = true;
            //        break;
            //    case Keys.D:
            //        textBox1.AppendText("Command Right Executed" + Environment.NewLine);
            //        _playerDirection = Direction.Right;
            //        e.Handled = true;
            //        break;
            //    case Keys.Space:
            //        textBox1.AppendText("Player shot." + Environment.NewLine);
            //        e.Handled = true;
            //        break;
            //    case Keys.C:
            //        createPlayer = true;
            //        Ginklas ginklas = new Granata(P1);
            //        textBox1.AppendText("Creating player:" + Environment.NewLine);
            //        e.Handled = true;
            //        break;
            //    case Keys.D1:
            //        ginklas = new Granata(P1);
            //        board.setGinklai(ginklas);
            //        textBox1.AppendText("Player switched to a grenade." + Environment.NewLine);
            //        e.Handled = true;
            //        break;
            //    case Keys.D2:
            //        ginklas = new Pistoletas(P1);
            //        board.setGinklai(ginklas);
            //        textBox1.AppendText("Player switched to a pistol." + Environment.NewLine);
            //        e.Handled = true;
            //        break;
            //    case Keys.D3:
            //        ginklas = new Automatas(P1);
            //        board.setGinklai(ginklas);
            //        textBox1.AppendText("Player switched to a assault rifle." + Environment.NewLine);
            //        e.Handled = true;
            //        break;
            //    case Keys.D4:
            //        ginklas = new Snaiperis(P1);
            //        board.setGinklai(ginklas);
            //        textBox1.AppendText("Player switched to a sniper." + Environment.NewLine);
            //        e.Handled = true;
            //        break;
            //    case Keys.D5:
            //        ginklas = new Bazuka(P1);
            //        board.setGinklai(ginklas);
            //        textBox1.AppendText("Player switched to a bazooka." + Environment.NewLine);
            //        e.Handled = true;
            //        break;
            //    case Keys.M:
            //        P1.UpdateHealth(-1);
            //        playerHit = true;
            //        textBox1.AppendText("Player got hit." + Environment.NewLine);
            //        observer.CheckHealth = P1;
            //        e.Handled = true;
            //        break;
            //    case Keys.P:
            //        textBox1.AppendText("Player opened shop." + Environment.NewLine);
            //        shop.Open(P1);
            //        e.Handled = true;
            //        break;
            //    case Keys.F1:
            //        textBox1.AppendText("Player Strategy set to Walk:" + Environment.NewLine);
            //        P1.setStrategy(moveAlgorithm);
            //        P1.Move(5.00f);
            //        e.Handled = true;
            //        break;
            //    case Keys.F2:
            //        textBox1.AppendText("Player Strategy set to Run:" + Environment.NewLine);
            //        P1.setStrategy(moveAlgorithm);
            //        P1.Move(10.00f);
            //        e.Handled = true;
            //        break;
            //}
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            _playerDirection = Direction.Stop;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            ////pagal nustatymus zaidimo laukas
            //e.Graphics.FillRectangle(Brushes.Black, 0, 0, 330, 330);
            //e.Graphics.FillRectangle(Brushes.White, 5, 5, 320, 320);

            ////zaidejo objektas
            //if (createPlayer)
            //{
                
            //    P1 = new PlayerFactory().GetPlayer();
            //    P1.PosY = 10;

            //    P1.PosX = 10;
            //    //textBox1.AppendText("Player Created: " + P1.Name + " HP: " + P1.health_points + " Gun: " + P1.defaultGun.SayHello() + Environment.NewLine);
            //    //e.Graphics.FillRectangle(Brushes.Aqua, P1.PosX, P1.PosY, 20, 20);
            //    createdPlayer = true;
            //    createPlayer = false;


            //    //pridedamas i observeriu sarasa
            //    observer.Attach(P1);
            //}
            //if (createdPlayer)
            //{
            //    e.Graphics.FillRectangle(Brushes.Aqua, P1.PosX, P1.PosY, 20, 20);
            //    e.Graphics.FillRectangle(Brushes.DarkGreen, 0, 400, P1.health_points, 50);
            //}

            ////observeriui
            //if (playerHit)
            //{
            //    e.Graphics.FillRectangle(Brushes.Red, 0, 400, 200, 50);
            //    e.Graphics.FillRectangle(Brushes.DarkGreen, 0, 400, P1.health_points, 50);

            //}

            ////abstract factory obsticales
            //if (obsticalescreate)
            //{
            //    List<string> ObsColors = new List<string> { "R", "G", "B" };
            //    List<int> Obscoordinates = new List<int>();


            //    for (int i = 10; i < 305; i = i + 1)
            //    {
            //        Obscoordinates.Add(i);
            //    }
            //    var random = new Random();
            //    for (int i = 0; i < 60; i++)

            //    {
            //        int index = random.Next(ObsColors.Count);
            //        obs = new ObsticaleFacotry().CreateObsticale(ObsColors[index]);
            //        obs.PosX = random.Next(Obscoordinates.Count);
            //        obs.PosY = random.Next(Obscoordinates.Count);
            //        int sk = 0;
            //        for (int j = 0; j < obsticaless.Count; j++)
            //        {
            //            if (obs.PosX >= (obsticaless[j].PosX -19) && obs.PosX <= (obsticaless[j].PosX + 20) && obs.PosY >= (obsticaless[j].PosY -19) && obs.PosY <= (obsticaless[j].PosY + 20))
            //            {
            //                sk++;
            //            }
            //        }
            //        if (sk == 0)
            //        {
            //            obsticaless.Add(obs);
            //        }




            //        if (index == 0)
            //        {
            //            // e.Graphics.FillRectangle(Brushes.Red, obsticaless.PosX, obsticaless.PosY, 10, 10);
            //        }
            //        if (index == 1)
            //        {
            //            // e.Graphics.FillRectangle(Brushes.Green, obsticaless.PosX, obsticaless.PosY, 10, 10);
            //        }
            //        if (index == 2)
            //        {
            //            // e.Graphics.FillRectangle(Brushes.Blue, obsticaless.PosX, obsticaless.PosY, 10, 10);
            //        }

            //    }

            //    obsCr = true;
            //    obsticalescreate = false;
            //    //obsticaless = new ObsticaleFacotry().CreateObsticale("R");

            //}
            //if (obsCr)
            //{
            //    foreach (Obsticale item in obsticaless)
            //    {
            //        if (item.Type == "Red")
            //        {
            //            e.Graphics.FillRectangle(Brushes.Red, item.PosX, item.PosY, 20, 20);
            //        }
            //        if (item.Type == "Green")
            //        {
            //            e.Graphics.FillRectangle(Brushes.Green, item.PosX, item.PosY, 20, 20);
            //        }
            //        if (item.Type == "Blue")
            //        {
            //            e.Graphics.FillRectangle(Brushes.Blue, item.PosX, item.PosY, 20, 20);
            //        }
            //    }
            //}

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (createdPlayer)
            //{
            //    //pagal nustatymus padaryti zaidimo lauka
            //    if (P1.PosX >= 5 && P1.PosX <= 305 && P1.PosY >= 5 && P1.PosY <= 305)
            //    {
            //        switch (_playerDirection)
            //        {
            //            case Direction.Right:
            //                RightCommand right = new RightCommand(P1);
            //                right.Execute();
            //                break;
            //            case Direction.Left:
            //                LeftCommand left = new LeftCommand(P1);
            //                left.Execute();
            //                break;
            //            case Direction.Up:
            //                UpCommand up = new UpCommand(P1);
            //                up.Execute();
            //                break;
            //            case Direction.Down:
            //                DownCommand down = new DownCommand(P1);
            //                down.Execute();
            //                break;
            //            case Direction.Stop:
            //                P1.PosX += 0;
            //                P1.PosY += 0;
            //                //textBox1.AppendText("x: " + P1.PosX + " y: " + P1.PosY + " " + Environment.NewLine);
            //                break;
            //        }
            //    }

            //}
            //Invalidate();
        }

        //note that paisyti reikia ant formos, o ne i picture

    }
}
