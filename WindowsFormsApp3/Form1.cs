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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        enum Direction
        {
            Left, Right, Up, Down, Stop
        }

        private Direction _playerDirection;
        private Player P1;
        private bool createPlayer = false;
        private bool createdPlayer = false;

        private List<Obsticale> obsticaless = new List<Obsticale>();
        private Obsticale obs;
        private bool obsticalescreate = false;
        private bool obsCr = false;
       // List<int> vienas = new List<int> { 1, 2, 3, 4, 5 };

        private ShopFacade shop = new ShopFacade();


        private bool playerHit = false;
        private CHP observer = new CHP();

        public Form1()
        {
            this.KeyPreview = true;

            //pradeda stovedamas
            _playerDirection = Direction.Stop;
            InitializeComponent();

            //nustatyti kiekvieno zaidejo pozicija atskiruose kampuose kai iseis su zaideju kazka padaryt
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Player player = new Player
            //{
            //    id = 1,
            //    Name = "a",
            //    health_points = 10,
            //    speed = 5,
            //    PosX = 10,
            //    PosY = 10
            //};
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N:
                    textBox1.AppendText("Started new game." + Environment.NewLine);
                    obsticalescreate = true;
                    break;
                case Keys.W:
                    //textBox1.AppendText("Player moved up." + Environment.NewLine);
                    _playerDirection = Direction.Up;
                    break;
                case Keys.S:
                    //textBox1.AppendText("Player moved down." + Environment.NewLine);
                    _playerDirection = Direction.Down;
                    break;
                case Keys.A:
                    //textBox1.AppendText("Player moved left." + Environment.NewLine);
                    _playerDirection = Direction.Left;
                    break;
                case Keys.D:
                    //textBox1.AppendText("Player moved right." + Environment.NewLine);
                    _playerDirection = Direction.Right;
                    break;
                case Keys.Space:
                    textBox1.AppendText("Player shot." + Environment.NewLine);
                    break;
                case Keys.C:
                    createPlayer = true;
                    textBox1.AppendText("Creating player:" + Environment.NewLine);
                    break;
                case Keys.M:
                    P1.UpdateHealth(-1);
                    playerHit = true;
                    textBox1.AppendText("Player got hit." + Environment.NewLine);
                    observer.CheckHealth = P1;
                    break;
                case Keys.P:
                    textBox1.AppendText("Player opened shop." + Environment.NewLine);
                    shop.Open(P1);
                    break;
                    //default:
                    //    _playerDirection = Direction.Stop;
                    //    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            textBox1.AppendText("Player stopped." + Environment.NewLine);
            _playerDirection = Direction.Stop;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //pagal nustatymus zaidimo laukas
            e.Graphics.FillRectangle(Brushes.Black, 0, 0, 330, 330);
            e.Graphics.FillRectangle(Brushes.White, 5, 5, 320, 320);

            //zaidejo objektas
            if (createPlayer)
            {
                P1 = new PlayerFactory().GetPlayer();
                P1.PosY = 10;
                P1.PosX = 10;
                textBox1.AppendText("Player Created: " + P1.Name + " HP: " + P1.health_points + " Gun: " + P1.defaultGun.SayHello() + Environment.NewLine);
                //e.Graphics.FillRectangle(Brushes.Aqua, P1.PosX, P1.PosY, 20, 20);
                createdPlayer = true;
                createPlayer = false;

                //pridedamas i observeriu sarasa
                observer.Attach(P1);
            }
            if (createdPlayer)
            {
                e.Graphics.FillRectangle(Brushes.Aqua, P1.PosX, P1.PosY, 20, 20);
                e.Graphics.FillRectangle(Brushes.Red, 0, 400, 200, 50);
            }

            //observeriui
            if (playerHit)
            {
                e.Graphics.FillRectangle(Brushes.DarkGreen, 0, 400, P1.health_points, 50);

            }
            //abstract factory obsticales
            if (obsticalescreate)
            {
                List<string> ObsColors = new List<string> { "R", "G", "B" };
                List<int> Obscoordinates = new List<int>();


                for (int i = 5; i < 300; i=i+1)
                {
                    Obscoordinates.Add(i);
                }
                var random = new Random();
                for (int i = 0; i < 60; i++)
               
                {
                    int index = random.Next(ObsColors.Count);
                    obs = new ObsticaleFacotry().CreateObsticale(ObsColors[index]);
                    obs.PosX = random.Next(Obscoordinates.Count);
                    obs.PosY = random.Next(Obscoordinates.Count);
                    obsticaless.Add(obs);

                    if (index == 0)
                    {
                       // e.Graphics.FillRectangle(Brushes.Red, obsticaless.PosX, obsticaless.PosY, 10, 10);
                    }
                    if (index == 1)
                    {
                       // e.Graphics.FillRectangle(Brushes.Green, obsticaless.PosX, obsticaless.PosY, 10, 10);
                    }
                    if (index == 2)
                    {
                       // e.Graphics.FillRectangle(Brushes.Blue, obsticaless.PosX, obsticaless.PosY, 10, 10);
                    }

                }

                obsCr = true;
                obsticalescreate = false;
                //obsticaless = new ObsticaleFacotry().CreateObsticale("R");
                
            }
            if (obsCr)
            {
                foreach (Obsticale item in obsticaless)
                {
                    if (item.Type == "Red")
                    {
                        e.Graphics.FillRectangle(Brushes.Red, item.PosX, item.PosY, 10, 10);
                    }
                    if(item.Type == "Green")
                    {
                        e.Graphics.FillRectangle(Brushes.Green, item.PosX, item.PosY, 10, 10);
                    }
                    if(item.Type == "Blue")
                    {
                        e.Graphics.FillRectangle(Brushes.Blue, item.PosX, item.PosY, 10, 10);
                    }
                }
            }
          

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (createdPlayer)
            {
                //pagal nustatymus padaryti zaidimo lauka
                if (P1.PosX >= 5 && P1.PosX <= 305 && P1.PosY >= 5 && P1.PosY <= 305)
                {
                    switch (_playerDirection)
                    {
                        case Direction.Right:
                            P1.PosX += (P1.PosX != 305 ? 5 : 0);
                            //textBox1.AppendText("x: " + P1.PosX + " y: " + P1.PosY + " " + Environment.NewLine);
                            break;
                        case Direction.Left:
                            P1.PosX -= (P1.PosX != 5 ? 5 : 0);
                            //textBox1.AppendText("x: " + P1.PosX + " y: " + P1.PosY + " " + Environment.NewLine);
                            break;
                        case Direction.Up:
                            P1.PosY -= (P1.PosY != 5 ? 5 : 0);
                            //textBox1.AppendText("x: " + P1.PosX + " y: " + P1.PosY + " " + Environment.NewLine);
                            break;
                        case Direction.Down:
                            P1.PosY += (P1.PosY != 305 ? 5 : 0);
                            //textBox1.AppendText("x: " + P1.PosX + " y: " + P1.PosY + " " + Environment.NewLine);
                            break;
                        case Direction.Stop:
                            P1.PosX += 0;
                            P1.PosY += 0;
                            //textBox1.AppendText("x: " + P1.PosX + " y: " + P1.PosY + " " + Environment.NewLine);
                            break;
                    }
                }

            }
            Invalidate();
        }


        //note that paisyti reikia ant formos, o ne i picture
       
        
    }
}
