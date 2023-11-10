using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serialization;

namespace FormsLib.Platformer
{
    public partial class formPlatformer : Form
    {
        private Stopwatch Time { get; set; }
        public formPlatformer()
        {
            InitializeComponent();
            this.KeyDown += Listener;
            Thread gameLoop = new Thread(() => GameLoop(ref Player));
            gameLoop.Start();
            this.FormClosed += (sender, e) =>
            {
                YouShouldBeDead = true;
            };
        }

        public bool YouShouldBeDead { get; set; } = false;


        private void GameLoop(ref PictureBox Player)
        {
            while (!YouShouldBeDead)
            {
                if (Player.Location.Y >= orangePlatform.Location.Y - (1.5 * orangePlatform.Height) - 3 ||
                    Player.Location.Y >= orangePlatform.Location.Y - (1.5 * orangePlatform.Height + 3))
                {
                    Player.Location = new(Player.Location.X, orangePlatform.Location.Y - Player.Height);
                    label1.Text = Player.Location.ToString();
                    continue;
                }

                IncrementY(-1);

                Thread.Sleep(50);
            }
        }



        private void Listener(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space) return;



            Random rng = new();
            IncrementY(20);
        }

        private void IncrementY(int amount) =>
            Player.Location = new(Player.Location.X, Player.Location.Y - amount);
        private void IncrementX(int amount) =>
            Player.Location = new(Player.Location.X - amount, Player.Location.Y);



    }
}
