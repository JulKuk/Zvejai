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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            canvas.FillRectangle(Brushes.Green, 10, 10, 100, 100);
            canvas.FillRectangle(Brushes.Orange, 5, 5, 20, 20);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.K)
            {
                textBox1.AppendText("labas");
                PlayerFactory player = new PlayerFactory();
                Player naujas = player.GetPlayer();
                string bel = naujas.Name;
                textBox1.AppendText(bel);
            }
        }
    }
}
