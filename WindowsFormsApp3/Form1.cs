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
using System.IO;

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
        //Bullets for shooting
        public List<Bullet> bulletsP1 = new List<Bullet>();
        public List<Bullet> bulletsP2 = new List<Bullet>();
        public int spacePressed = 0;
		enum Direction
		{
			Left, Right, Up, Down, Stop
		}

        enum Colour
        { 
            Red, Blue, Green
        }

		private Direction _playerDirection;
		private Direction _shotDirection;
		MoveAlgorithm moveAlgorithm = new MoveAlgorithm();

		public ICollection<Player> Players;
		public Player P1;
		public Player P2;
		private long x, y;
		private Player CurrentPlayer;

		private bool P1Connected, P2Connected = false;
		private bool shoot,shooting = false;
        

		private List<Obsticale> obsticaless = new List<Obsticale>();
		private int[] kordinates = new int[20];
		
		private Obsticale obsR = new ObsticaleFacotry().CreateObsticale("R");
		private Obsticale obsB = new ObsticaleFacotry().CreateObsticale("B");
		private Obsticale obsG = new ObsticaleFacotry().CreateObsticale("G");
		private Obsticale obsatskiras;
		private bool obsticalescreate = false;
		private bool obsCr = false;
		// List<int> vienas = new List<int> { 1, 2, 3, 4, 5 };

		private ShopFacade shop = new ShopFacade();

		Gameboard board = new Gameboard();

		private bool playerHit = false;
		private CHP observer = new CHP();

		private List<PictureBox> testObstacles = new List<PictureBox>();



		public Form1()
		{
			this.KeyPreview = true;

			//pradeda stovedamas
			_playerDirection = Direction.Stop;

			InitializeComponent();
		}

		async Task<ICollection<Obsticale>> GetAllObstaclesAsync(string path)
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

		async Task<Uri> CreateObstacleAsync(Obsticale obstacle)
		{
			var s = new StringContent(JsonConvert.SerializeObject(obstacle), Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync("https://localhost:44371/api/obstacles", new StringContent(JsonConvert.SerializeObject(obstacle), Encoding.UTF8, "application/json"));
			response.EnsureSuccessStatusCode();

			// Deserialize the updated product from the response body.
			var obstacle2 = await response.Content.ReadAsStringAsync();


			// return URI of the created resource.
			return response.Headers.Location;
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

		async Task<Player> GetPlayerAsync(string path, long id)
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

		async Task<Uri> UpdatePlayerAsync(Player player)
		{
			var s = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync(path + "api/player/" + player.id, new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json"));
			response.EnsureSuccessStatusCode();

			// Deserialize the updated product from the response body.
			var player2 = await response.Content.ReadAsStringAsync();


			// return URI of the created resource.
			return response.Headers.Location;
		}
		private async void Connect()
		{
            spacePressed = 0;
			if (Players.Count == 0)
			{
				Player p = PlayerFactory.GetPlayer();
				p.PosX = 20;
				p.PosY = 50;
				p.speed = 10;
				var url = await CreatePlayerAsync(p);
				CurrentPlayer = await GetPlayerAsync(path, 1);
				textBox1.AppendText("Player 1 Connected.");
                P1Connected = true;
                for (int i = 0; i < CurrentPlayer.Weapon.ammo; i++)
                {
                    Bullet temp = new Bullet { bulletID = i, posX = CurrentPlayer.PosX, posY = CurrentPlayer.PosY, speed = 10 };
                    bulletsP1.Add(temp);
                }
            }
			else if (Players.Count == 1)
			{
				Player p = PlayerFactory.GetPlayer();
				p.PosX = 50;
				p.PosY = 200;
				p.speed = 10;
				var url = await CreatePlayerAsync(p);
				CurrentPlayer = await GetPlayerAsync(path, 2);
				textBox1.AppendText("Player 2 Connected.");
				P2Connected = true;
                for (int i = 0; i < CurrentPlayer.Weapon.ammo; i++)
                {
                    Bullet temp = new Bullet{ bulletID = i, posX = CurrentPlayer.PosX, posY = CurrentPlayer.PosY, speed = 10 };
                    bulletsP2.Add(temp);
                }
			}

			ObsticalesBraizymas();


			// Deserialize the updated product from the response body.


			// return URI of the created resource.
		}

		private void ObsticalesBraizymas()
		{
            Random rnd = new Random();
            Array colours = Enum.GetValues(typeof(Colour));

			int[] xai = new int[6];
			int[] yai = new int[6];

            //kazkaip kitaip reikia nurodyt i to failo vieta
            string path = Path.GetFullPath("WindowsFormsApp3.exe").ToString();
            string path2 = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\"));
            string path3 = Path.GetFullPath(Path.Combine(path2, @"obsord.txt"));
            using (TextReader reader = File.OpenText(path3))
            {
                for (int i = 0; i < 6; i++)
                {
                    xai[i] = int.Parse(reader.ReadLine());
                    yai[i] = int.Parse(reader.ReadLine());
                }
            }
			List<Obsticale> visos = new List<Obsticale>();
			visos.Add(obsB);
			visos.Add(obsG);
			visos.Add(obsR);

			for (int i = 0; i < 6; i++)
			{
				obsatskiras = (Obsticale)obsR.Clone();
				obsatskiras.PosX = xai[i];
				obsatskiras.PosY = yai[i];
				obsticaless.Add(obsatskiras);

                GenerateObstacle(Convert.ToInt32(obsatskiras.PosX), Convert.ToInt32(obsatskiras.PosY), 20, 20, (Colour)colours.GetValue(rnd.Next(colours.Length)));
				//var url = await CreateObstacleAsync(obsatskiras);
			}
			obsticalescreate = true;
		}

		private async void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				//case Keys.N:
				//    textBox1.AppendText("Started new game." + Environment.NewLine);
				//    obsticalescreate = true;
				//    e.Handled = true;
				//    break;
				case Keys.W:
					textBox1.AppendText("Command Up Executed" + Environment.NewLine);
					_playerDirection = Direction.Up;
					_shotDirection = Direction.Up;
					e.Handled = true;
					break;
				case Keys.S:
					textBox1.AppendText("Command Down Executed" + Environment.NewLine);
					_playerDirection = Direction.Down;
					_shotDirection = Direction.Down;
					e.Handled = true;
					break;
				case Keys.A:
					textBox1.AppendText("Command Left Executed" + Environment.NewLine);
					_playerDirection = Direction.Left;
					_shotDirection = Direction.Left;
					e.Handled = true;
					break;
				case Keys.D:
					textBox1.AppendText("Command Right Executed" + Environment.NewLine);
					_playerDirection = Direction.Right;
					_shotDirection = Direction.Right;
					e.Handled = true;
					break;
				case Keys.Space:
                    CurrentPlayer = await GetPlayerAsync(path, CurrentPlayer.id);
                    if(CurrentPlayer.Weapon.ammo  > spacePressed)
                    {
                        shoot = true;
                        bulletsP1[spacePressed].posX = CurrentPlayer.PosX;
                        bulletsP1[spacePressed].posY = CurrentPlayer.PosY;
                        bulletsP1[spacePressed].visible = true;
                        spacePressed++;
                        e.Handled = true;
                    }else
                    {
                        textBox1.AppendText("OutOfAmmo"+ Environment.NewLine);
                        e.Handled = true;
                    }
					break;
                case Keys.R:
                    spacePressed = 0;
                    break;
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
            }
         }

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			_playerDirection = Direction.Stop;
            if (spacePressed <= CurrentPlayer.Weapon.ammo)
            {
                if (e.KeyCode == Keys.Space)
                {
                    spacePressed--;
                }
            }
		}
		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			//pagal nustatymus zaidimo laukas
			e.Graphics.FillRectangle(Brushes.Black, 0, 0, 330, 330);
			e.Graphics.FillRectangle(Brushes.White, 10, 10, 310, 310);
			if (P1 != null)
			{
				e.Graphics.FillRectangle(Brushes.DarkOrange, P1.PosX, P1.PosY, 10, 10);
                if (spacePressed != P1.Weapon.ammo)
                {
                    if (bulletsP1[spacePressed].visible)
                    {
                        for (int i = 0; i < bulletsP1.Count; i++)
                        {
                            if (bulletsP1[i].visible)
                            {
                                if (bulletsP1[i].posX < 325)
                                {
                                    e.Graphics.FillRectangle(Brushes.Black, bulletsP1[i].posX, bulletsP1[i].posY, 5, 5);
                                }
                                else
                                {
                                    bulletsP1[i].visible = false;
                                }
                            }
                        }

                    }
                }
            }
			if (P2 != null)
			{
				e.Graphics.FillRectangle(Brushes.Red, P2.PosX, P2.PosY, 10, 10);
			}

            
			//if (obsticalescreate)
			//{
			//	for (int i = 0; i < 6; i++)
			//	{
			//		e.Graphics.FillRectangle(Brushes.Red, obsticaless[i].PosX, obsticaless[i].PosY, 10, 10);
			//	}
			//}
			

            //kliutys
            //GenerateObstacle();
            //var obstacleBox = new Rectangle(100, 100, 100, 100);


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

		private async void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Enabled = false;
			timer1.Interval = 100;
			timer1.Enabled = true;


			P1 = await GetPlayerAsync(path, 1);
			P2 = await GetPlayerAsync(path, 2);

			if (P1Connected)
			{
				var Random2 = PlayerMovement(P1);
                await UpdatePlayerAsync(Random2);
            }
			if (P2Connected)
			{
				var Random = PlayerMovement(P2);
				await UpdatePlayerAsync(Random);
			}
			if (shoot)
			{
                for (int i = 0; i < bulletsP1.Count; i++)
                {
                    bulletsP1[i].posX += 10;
                }
                    
			}
			Invalidate();
		}

		//note that paisyti reikia ant formos, o ne i picture
		public Player PlayerMovement(Player CurrentPlayer)
		{
			if (CurrentPlayer.PosX >= 10 && CurrentPlayer.PosX <= 310 && CurrentPlayer.PosY >= 10 && CurrentPlayer.PosY <= 310)
			{
				switch (_playerDirection)
				{
					case Direction.Right:
						if (!CollisionDetection(CurrentPlayer, Direction.Right) && CurrentPlayer.PosX != 310)
						{
							RightCommand right = new RightCommand(CurrentPlayer);
							right.Execute();
							break;
						}
						break;
					case Direction.Left:
						if (!CollisionDetection(CurrentPlayer, Direction.Left) && CurrentPlayer.PosX != 10)
						{
							LeftCommand left = new LeftCommand(CurrentPlayer);
							left.Execute();
							break;
						}
						break;
					case Direction.Up:
						if (!CollisionDetection(CurrentPlayer, Direction.Up) && CurrentPlayer.PosY != 10)
						{
							UpCommand up = new UpCommand(CurrentPlayer);
							up.Execute();
							break;
						}
						break;
					case Direction.Down:
						if (!CollisionDetection(CurrentPlayer, Direction.Down) && CurrentPlayer.PosY != 310)
						{
							DownCommand down = new DownCommand(CurrentPlayer);
							down.Execute();
							break;
						}
						break;
					case Direction.Stop:
						CurrentPlayer.PosX += 0;
						CurrentPlayer.PosY += 0;
						//textBox1.AppendText("x: " + P1.PosX + " y: " + P1.PosY + " " + Environment.NewLine);
						break;
				}
			}
			return CurrentPlayer;
		}

		private bool CollisionDetection(Player player, Direction direction)
		{
			bool hits = false;
			foreach (PictureBox pb in testObstacles)
			{
				var pictureRect = new Rectangle(
					pb.Location.X,
					pb.Location.Y,
					pb.Width,
					pb.Height
				);


				switch (direction)
				{
					case Direction.Right:
						hits = pictureRect.Contains(Convert.ToInt32(player.PosX) + board.step, Convert.ToInt32(player.PosY));
						break;
					case Direction.Left:
						hits = pictureRect.Contains(Convert.ToInt32(player.PosX) - board.step, Convert.ToInt32(player.PosY));
						break;
					case Direction.Up:
						hits = pictureRect.Contains(Convert.ToInt32(player.PosX), Convert.ToInt32(player.PosY) - board.step);
						break;
					case Direction.Down:
						hits = pictureRect.Contains(Convert.ToInt32(player.PosX), Convert.ToInt32(player.PosY) + board.step);
						break;
				}
				if (hits)
				{
					return hits;
				}
			}
			return hits;
		}

		private void GenerateObstacle(int posX, int posY, int width, int height, Colour colour)
		{
            string colourFile;
            switch (colour)
            {
                case Colour.Blue:
                    colourFile = "Image/obstacleBlue.png";
                    break;
                case Colour.Green:
                    colourFile = "Image/obstacleGreen.png";
                    break;
                case Colour.Red:
                    colourFile = "Image/obstacleRed.png";
                    break;
                default:
                    colourFile = "Image/obstacleRed.png";
                    break;
            }

			var obstacleBox = new PictureBox
			{
				Name = "obstacleBox",
				Size = new Size(width, height),
				Location = new Point(posX, posY),
				Image = Image.FromFile(colourFile),
				SizeMode = PictureBoxSizeMode.StretchImage
		    };
            //obsticaless.Add(obsR);
            testObstacles.Add(obstacleBox);
			Controls.Add(obstacleBox);
		}
	}
}
