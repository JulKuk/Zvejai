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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        enum Direction
        {
            Left, Right, Up, Down, Stop
        }

        private int _x; //situos reikia is playerio paimt
        private int _y; //situos reikia is playerio paimt
        private Direction _playerDirection;
        

        public Form1()
        {
            this.KeyPreview = true;

            //pradeda stovedamas
            _playerDirection = Direction.Stop;

            InitializeComponent();

            //nustatyti kiekvieno zaidejo pozicija atskiruose kampuose kai iseis su zaideju kazka padaryt
            _x = 50;
            _y = 50;
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
            //kol kas neveikia kai keli mygtukai vienu metu spaudziami
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N:
                    textBox1.AppendText("Started new game." + Environment.NewLine);
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


        private void timer1_Tick(object sender, EventArgs e)
        {
            //pagal nustatymus padaryti zaidimo lauka
            if (_x >= 5 && _x <= 305 && _y >= 5 && _y <= 305)
            {
                switch (_playerDirection)
                {
                    case Direction.Right:
                        _x += (_x != 305 ? 5 : 0);
                        //textBox1.AppendText("x: " + _x + " y: " + _y + " " + Environment.NewLine);
                        break;
                    case Direction.Left:
                        _x -= (_x != 5 ? 5 : 0);
                        //textBox1.AppendText("x: " + _x + " y: " + _y + " " + Environment.NewLine);
                        break;
                    case Direction.Up:
                        _y -= (_y != 5 ? 5 : 0);
                        //textBox1.AppendText("x: " + _x + " y: " + _y + " " + Environment.NewLine);
                        break;
                    case Direction.Down:
                        _y += (_y != 305 ? 5 : 0);
                        //textBox1.AppendText("x: " + _x + " y: " + _y + " " + Environment.NewLine);
                        break;
                    case Direction.Stop:
                        _x += 0;
                        _y += 0;
                        //textBox1.AppendText("x: " + _x + " y: " + _y + " " + Environment.NewLine);
                        break;
                }
            }

            Invalidate();
        }

        //note that paisyti reikia ant formos, o ne i picture
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //pagal nustatymus zaidimo laukas
            e.Graphics.FillRectangle(Brushes.Black, 0, 0, 330, 330);
            e.Graphics.FillRectangle(Brushes.White, 5, 5, 320, 320);

            //zaidejo objektas
            e.Graphics.FillRectangle(Brushes.Orange, _x, _y, 20, 20);
        }
    }
}
