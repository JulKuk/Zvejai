﻿using System;
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
        MoveAlgorithm moveAlgorithm = new MoveAlgorithm();
        private Player P1;
        private bool createPlayer = false;
        private bool createdPlayer = false;

        private ShopFacade shop = new ShopFacade();


        private bool playerHit = false;
        private CHP observer = new CHP();

        public Form1()
        {
            this.KeyPreview = true;

            //pradeda stovedamas
            _playerDirection = Direction.Stop;

            InitializeComponent();
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
                case Keys.F1:
                    textBox1.AppendText("Player Strategy set to Walk:" + Environment.NewLine);
                    P1.setStrategy(moveAlgorithm);
                    P1.Move(5.00f);
                    break;
                case Keys.F2:
                    textBox1.AppendText("Player Strategy set to Run:" + Environment.NewLine);
                    P1.setStrategy(moveAlgorithm);
                    P1.Move(10.00f);
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
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
                e.Graphics.FillRectangle(Brushes.DarkGreen, 0, 400, P1.health_points, 50);
            }

            //observeriui
            if (playerHit)
            {
                e.Graphics.FillRectangle(Brushes.Red, 0, 400, 200, 50);
                e.Graphics.FillRectangle(Brushes.DarkGreen, 0, 400, P1.health_points, 50);

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
                            long nextCoord = P1.PosX + Convert.ToInt64(P1.speed);
                            if (nextCoord <= 305){
                                P1.PosX += Convert.ToInt64(P1.speed);
                            }
                            else
                            {
                                long diff = 305 - P1.PosX;
                                P1.PosX += diff;
                            }
                            //textBox1.AppendText("x: " + P1.PosX + " y: " + P1.PosY + " " + Environment.NewLine);
                            break;
                        case Direction.Left:
                            nextCoord = P1.PosX - Convert.ToInt64(P1.speed);
                            if (nextCoord >= 5)
                            {
                                P1.PosX -= Convert.ToInt64(P1.speed);
                            }
                            else
                            {
                                long diff = P1.PosX - 5;
                                P1.PosX -= diff;
                            }
                            //textBox1.AppendText("x: " + P1.PosX + " y: " + P1.PosY + " " + Environment.NewLine);
                            break;
                        case Direction.Up:
                            nextCoord = P1.PosY - Convert.ToInt64(P1.speed);
                            if (nextCoord >= 5)
                            {
                                P1.PosY -= Convert.ToInt64(P1.speed);
                            }
                            else
                            {
                                long diff = P1.PosY - 5;
                                P1.PosY -= diff;
                            }
                            //textBox1.AppendText("x: " + P1.PosX + " y: " + P1.PosY + " " + Environment.NewLine);
                            break;
                        case Direction.Down:
                            nextCoord = P1.PosY + Convert.ToInt64(P1.speed);
                            if (nextCoord <= 305)
                            {
                                P1.PosY += Convert.ToInt64(P1.speed);
                            }
                            else
                            {
                                long diff = 305 - P1.PosY;
                                P1.PosY += diff;
                            }
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
