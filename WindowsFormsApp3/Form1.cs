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
using GameServer.Models.Iterator;
using GameServer.Models.Flyweight;
using GameServer.Models.Memento;
using WindowsFormsApp3;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using GameServer.Models.Visitor;
using GameServer.Models.Adapter;

namespace WindowsFormsApp1
{
	public partial class 
        Form1 : Form
	{

		//Adapteris
		ReloadWeaponAdapter adapter = new ReloadWeaponAdapter();
		
        //Http Klientas jsonui siusti

        //Fascade naudojimas serveriukui
        private GameFacade GameFacade = new GameFacade();
		enum Direction
		{
			Left, Right, Up, Down, Stop
		}
        
        enum Colour
        { 
            Red, Blue, Green
        }
		private CommandVisitor commandVisitor = new CommandVisitor();
        private Direction _lastDirection;
        private Direction _playerDirection;

		public ICollection<Player> Players;
        public ICollection<Bullet> bullets;
		public Player P1;
		public Player P2;

        private int _currentPlayerId;
        private Player CurrentPlayer => _currentPlayerId == 1 ? P1 : P2;

		private bool P1Connected, P2Connected = false;
        

		private List<Obsticale> obsticaless = new List<Obsticale>();
		
		private Obsticale obsR = new ObsticaleFacotry().CreateObsticale("R");
		private Obsticale obsB = new ObsticaleFacotry().CreateObsticale("B");
		private Obsticale obsG = new ObsticaleFacotry().CreateObsticale("G");
		private Obsticale obsatskiras;
		private FlyFactory fl = new FlyFactory();
		private Bazooka bazuka = new Bazooka();
		private Weapons2 wpns = new Weapons2();
        // List<int> vienas = new List<int> { 1, 2, 3, 4, 5 

        Memento.Caretaker ct = new Memento.Caretaker();

		private ShopFacade shop = new ShopFacade();

		Gameboard board = new Gameboard();

		private CHP observer = new CHP();

		private List<PictureBox> testObstacles = new List<PictureBox>();

        private Memento.Originator org;

        private Timer timer1;

        public Form1()
		{
            

            //textBox1.AppendText(bazuka.DisplayInventory());

            this.KeyPreview = true;
            timer1 = new Timer();
			//pradeda stovedamas
			_playerDirection = Direction.Stop;

			InitializeComponent();
		}

		

		private async void Form1_Load(object sender, EventArgs e)
		{
            Players = await GameFacade.GetAllPlayersFromDatabase();
            Connect();
            Players = await GameFacade.GetAllPlayersFromDatabase();
            timer1.Tick += timer1_Tick;
            timer1.Start();
            timer1.Interval = 100;

            // Composite ----------------------------------------------
            var salmas = new Helmet();
            var sautuvas = new Pistol();
            bazuka.Inventory.Add(sautuvas);
            bazuka.Inventory.Add(salmas);
            bazuka.Inventory.Add(bazuka);
            int i = 0;

            foreach (var t in bazuka.Inventory)
            {
                textBox1.AppendText(t.SayHello() + Environment.NewLine);
				wpns[i++] = t.SayHello();
            }

            // ---------------------------------------------------------
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
		
        private async void Connect()
		{
            if (Players.Count == 0)
            {
                
				 await GameFacade.AddPlayerToDatabase(GameFacade.GetGame().CreatePlayer(1));
                _currentPlayerId = 1;
				textBox1.AppendText("Player 1 Connected.");
                P1Connected = true;
                setMemento(CurrentPlayer);
            }
            else if (Players?.Count == 1)
			{
				
				await GameFacade.AddPlayerToDatabase(GameFacade.GetGame().CreatePlayer(2));
                _currentPlayerId = 2;
				textBox1.AppendText("Player 2 Connected.");
                P2Connected = true;
                setMemento(CurrentPlayer);
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
			char[] chars = { 'R', 'B' ,'G', 'B', 'G','R'};
			
			
			int total = 0;

			foreach (char c in chars)
			{				
				FlyWeightObsticale chr = fl.GetObs(c);
				chr.PosX = xai[total];
				chr.PosY = yai[total];
				fl[total] = chr.Type;
				GenerateObstacle(Convert.ToInt32(chr.PosX), Convert.ToInt32(chr.PosY), 20, 20, (Colour)colours.GetValue(rnd.Next(colours.Length)));
				total++;
			}

			/*for (int i = 0; i < 6; i++)
			{
				obsatskiras = (Obsticale)obsR.Clone();
				obsatskiras.PosX = xai[i];
				obsatskiras.PosY = yai[i];
				obsticaless.Add(obsatskiras);

                GenerateObstacle(Convert.ToInt32(obsatskiras.PosX), Convert.ToInt32(obsatskiras.PosY), 20, 20, (Colour)colours.GetValue(rnd.Next(colours.Length)));
				//var url = await CreateObstacleAsync(obsatskiras);
			}*/
		}

		private async void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
                case Keys.N:
                    break;
                case Keys.W:
					_playerDirection = Direction.Up;
					e.Handled = true;
					break;
				case Keys.S:
					_playerDirection = Direction.Down;
					e.Handled = true;
					break;
				case Keys.A:
					_playerDirection = Direction.Left;
					e.Handled = true;
					break;
				case Keys.D:
					_playerDirection = Direction.Right;
					e.Handled = true;
					break;
				case Keys.Space:
					textBox1.AppendText(CurrentPlayer.Weapon._kiekKulkuYra.ToString() + Environment.NewLine);
                    var bullet = CurrentPlayer.Shoot();
                    if (bullet == null)
                    {
                        textBox1.AppendText("OutOfAmmo" + Environment.NewLine);
                        return;
                    }
                    bullet.shootingDir = GetBulletDirection();
                    await GameFacade.AddBulletToDatabase(bullet);
                    e.Handled = true;

					break;
                case Keys.R:
					adapter.changePlayerState(CurrentPlayer);
                    await GameFacade.UpdatePlayerToDatabase(CurrentPlayer);
                    e.Handled = true;
                    break;
				case Keys.P:					
					Iterator i = wpns.CreateIterator();
					object item = i.First();
					textBox1.AppendText("Turimi ginklai");
					while(item != null)
					{
						textBox1.AppendText(" " + item);
						item = i.Next();
					}
					break;
				case Keys.O:
					Iterator u = fl.CreateIterator();
					object item2 = u.First();
					textBox1.AppendText("Sukurtos kliutys");
					while (item2 != null)
					{
						textBox1.AppendText(" " + item2);
						item2 = u.Next();
					}
					break;
                case Keys.K:
                    CurrentPlayer.addPoints(100);
                    e.Handled = true;
                    break;
                case Keys.L:
                    org.setPoints(CurrentPlayer.points - 100);
                    break;
            }
         }

        private Bullet.Direction GetBulletDirection()
        {
            switch (_lastDirection)
            {
                case Direction.Down: return Bullet.Direction.Down; 
                case Direction.Up: return Bullet.Direction.Up; 
                case Direction.Left: return Bullet.Direction.Left; 
                case Direction.Right: return Bullet.Direction.Right; 
                default: return Bullet.Direction.Right;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
            switch (e.KeyCode)
            {
                case Keys.W:
                    _lastDirection = _playerDirection;
                    _playerDirection = Direction.Stop;
                    e.Handled = true;
                    break;
                case Keys.S:
                    _lastDirection = _playerDirection;
                    _playerDirection = Direction.Stop;
                    e.Handled = true;
                    break;
                case Keys.A:
                    _lastDirection = _playerDirection;
                    _playerDirection = Direction.Stop;
                    e.Handled = true;
                    break;
                case Keys.D:
                    _lastDirection = _playerDirection;
                    _playerDirection = Direction.Stop;
                    e.Handled = true;
                    break;
            }
			
		}
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //pagal nustatymus zaidimo laukas
            e.Graphics.FillRectangle(Brushes.Black, 0, 0, 900, 330);
            e.Graphics.FillRectangle(Brushes.White, 10, 10, 880, 310);

            if (P1 != null)
            {
                e.Graphics.FillRectangle(Brushes.DarkOrange, P1.PosX, P1.PosY, 30, 30);
            }

            if (P2 != null)
            {
                e.Graphics.FillRectangle(Brushes.Red, P2.PosX, P2.PosY, 30, 30);
            }

            if(bullets != null)
            {
                foreach (var item in bullets)
                {
                    if (item.visible)
                    {
                        e.Graphics.FillRectangle(Brushes.Black, item.posX, item.posY, 5, 5);
                    }
                }
            }

        }

		private async void timer1_Tick(object sender, EventArgs e)
		{
            P1 = await GameFacade.GetPlayerByID(1);
            P2 = await GameFacade.GetPlayerByID(2);
            bullets = await GameFacade.GetAllBulletsFromDatabase();

            if (P1Connected)
            {
				if(P1.points == 0 || P1.points == 100)
				{
					//textBox1.AppendText("Player speed " + P1.speed + Environment.NewLine);
				}
				if (P1.points == 300)
				{
					textBox1.AppendText("Player Ammo " + P1.Weapon._kiekKulkuYra + Environment.NewLine);
				}
				var Random2 = PlayerMovement(P1);
                await GameFacade.UpdatePlayerToDatabase(Random2);

            }

            if (P2Connected)
            {
                var Random = PlayerMovement(P2);
                await GameFacade.UpdatePlayerToDatabase(Random);
            }

            if (P1 != null && P2 != null)
            {
                if(P1.health_points <= 0)
                {
                    P1.health_points = 100;
                    P1.PosY = 50;
                    P1.PosX = 20;
                    await GameFacade.UpdatePlayerToDatabase(P1);
                    textBox1.AppendText("P2 Killed P1 Player2 Score:" + P2.points.ToString() + Environment.NewLine);
                }
                if(P2.health_points <= 0)
                {
                    P2.health_points = 100;
                    P2.PosX = 200;
                    P2.PosY = 200;
                    await GameFacade.UpdatePlayerToDatabase(P2);
                    textBox1.AppendText("P1 Killed P2 P1 Score:" + P1.points.ToString() + Environment.NewLine); ;
                }
                if(P2.Score == 4 || P1.Score >= 4)
                {
                    if(P2.Score > P1.Score)
                    {
                        textBox1.AppendText("Player2 Wins the game with " + P2.Score);
                        textBox1.AppendText("Player1 points " + P1.Score);
                        timer1.Enabled = false;
                    }
                    else
                    {
                        textBox1.AppendText("Player1 Wins the game with " + P1.Score);
                        textBox1.AppendText("Player2 points " + P2.Score);
                        timer1.Enabled = false;
                    }
                }
            }
            Invalidate();
            Refresh();
                    
		
			
		}

		//note that paisyti reikia ant formos, o ne i picture
		public Player PlayerMovement(Player CurrentPlayer)
		{
			if (CurrentPlayer.PosX >= 10 && CurrentPlayer.PosX <= 880 && CurrentPlayer.PosY >= 10 && CurrentPlayer.PosY <= 310)
			{
				switch (_playerDirection)
				{
					case Direction.Right:
						if (!CollisionDetection(CurrentPlayer, Direction.Right))
						{
							commandVisitor.visit(new RightCommand(CurrentPlayer));
							break;
						}
						break;
					case Direction.Left:
						if (!CollisionDetection(CurrentPlayer, Direction.Left))
						{
							commandVisitor.visit(new LeftCommand(CurrentPlayer));
							break;
						}
						break;
					case Direction.Up:
						if (!CollisionDetection(CurrentPlayer, Direction.Up))
						{
							commandVisitor.visit(new UpCommand(CurrentPlayer));
							break;
						}
						break;
					case Direction.Down:
						if (!CollisionDetection(CurrentPlayer, Direction.Down))
						{
							commandVisitor.visit(new DownCommand(CurrentPlayer));
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
            testObstacles.Add(obstacleBox);
			Controls.Add(obstacleBox);
		}
        public void setMemento(Player P)
        {
             org = new Memento.Originator(P.points);
            Memento.IntefaceMemento meme = org.savePoints();
            ct.add(meme);
        }

    }

    
}
