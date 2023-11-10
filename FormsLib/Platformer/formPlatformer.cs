using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serialization;

namespace FormsLib.Platformer
{
    public partial class formPlatformer : Form
    {
        private float _targetX = 0;
        private float _lerpX = 0.5f;

        private float _friction = 0.2f;

        private float _velocityY = 0;
        private float _velocityX = 0;
        private bool _isJumping = false;
        private bool _formClosed = false;

        private bool _movingLeft = false;
        private bool _movingRight = false;

        public formPlatformer()
        {
            InitializeComponent();

            KeyDown += KeyDownListener;
            KeyUp += KeyUpListener;

            Thread gameLoop = new Thread(() => GameLoop(ref Player));

            gameLoop.Start();

            FormClosed += (sender, e) =>
            {
                _formClosed = true;
            };
        }


        private void GameLoop(ref PictureBox Player)
        {
            while (!_formClosed)
            {
                // Gravity
                _velocityY -= 0.2f;

                // Side Speed
                _velocityX = _velocityX + ( _targetX - _velocityX ) * _lerpX;

                if(_velocityY !=  0)
                    _velocityX = _velocityX - (_friction * _velocityX);

                label1.Text = ($"Y: {_velocityY}, X: {_velocityX}");

                if (Player.Location.X >= wallRight.Location.X) {
                    _velocityX = 0f;
                    Player.Location = new(wallRight.Location.X - (wallRight.Width / 2), Player.Location.Y);
                }

                if (Player.Location.X <= wallLeft.Location.X + wallLeft.Right) {
                    _velocityX = 0f;
                    Player.Location = new(wallLeft.Location.X + wallLeft.Right + 1, Player.Location.Y);
                }

                MovePlayer();


                // Handles Jumping

                if (Player.Location.Y >= orangePlatform.Location.Y - (1.5 * orangePlatform.Height) + 2 ||
                    Player.Location.Y >= orangePlatform.Location.Y - (1.5 * orangePlatform.Height) - 2)
                {
                    _isJumping = false;
                    Player.Location = new(Player.Location.X, orangePlatform.Location.Y - Player.Height);
                    _velocityY = 0f;
                }



                Thread.Sleep(5);
            }
        }


        private void MovePlayer()
        {
            Player.Location = new(Player.Location.X + (int)_velocityX, Player.Location.Y - (int)_velocityY);
        }

        private void KeyDownListener(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                case Keys.W:
                    if (_isJumping) return;
                    _isJumping = true;
                    _velocityY += 8;
                    break;

                case Keys.A:
                    _movingLeft = true;
                    _targetX = -10f;
                    break;

                case Keys.D:
                    _movingRight = true;
                    _targetX = 10f;
                    break;

                default:
                    break;
            }

        }

        private void KeyUpListener(object? sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.A:
                    if (_movingRight) _targetX = 10f;
                    else _targetX = 0f;
                    _movingLeft = false;
                    break;
                case Keys.D:
                    if (_movingLeft) _targetX = -10f;
                    else _targetX = 0f;
                    _movingRight = false;
                    break;
                default:
                    break;
                    
            }
        }
    }
}
