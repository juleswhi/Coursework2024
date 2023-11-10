namespace FormsLib.Platformer;

internal class Game
{
    private float _targetX = 0;
    private float _moveRateX = 0.5f;

    private float _friction = 0.2f;
    private float _gravity = 0.2f;
    private float _jumpHeight = 8f;

    private float _velocityX = 0;
    private float _velocityY = 0;
    private bool _isJumping = false;
    private bool _formClosed = false;

    private bool _movingLeft = false;
    private bool _movingRight = false;

    private bool _jumpNext = false;


    private PictureBox _player;
    private List<PictureBox> _platforms = new();


    public Game(Form form, PictureBox Player)
    {
        foreach (var control in form.Controls)
        {
            if (control is PictureBox)
            {
                if ((control as PictureBox)!.Tag == "Player") continue;
                _platforms.Add(control as PictureBox);
            }
        }
        _player = Player;

        form.KeyDown += KeyDownListener;
        form.KeyUp += KeyUpListener;

        Thread gameLoop = new Thread(GameLoop);

        gameLoop.Start();

        form.FormClosed += (sender, e) => _formClosed = true;
        _player.Enabled = true;
    }

    private void GameLoop()
    {
        while (!_formClosed)
        {

            _velocityX = _velocityX + (_targetX - _velocityX) * _moveRateX;

            if (_velocityY != 0)
                _velocityX = _velocityX - (_friction * _velocityX);


            foreach (var platform in _platforms)
            {
                // Coming from RHS
                if(_player.Location.X <= platform.Location.X + platform.Width && 
                    _player.Location.X + 5 > platform.Location.X)
                {
                    if (_player.Bottom < platform.Location.Y + 2) continue;
                    if(_player.Top > platform.Location.Y + platform.Height ) continue;

                    if (_movingLeft)
                        _velocityX = 0f;

                    _player.Location = new(platform.Location.X + platform.Width, _player.Location.Y);
                }

                // Coming from LHS 
                if(_player.Location.X + _player.Width >= platform.Location.X &&
                    _player.Location.X - 5 < platform.Location.X )
                {
                    if (_player.Bottom < platform.Location.Y + 2) continue;
                    if(_player.Top > platform.Location.Y + platform.Height) continue;

                    if(_movingRight) 
                        _velocityX = 0f;
                    _player.Location = new(platform.Location.X - _player.Width, _player.Location.Y);
                }
            }

            MovePlayer();
            
            
            foreach(var platform in _platforms)
            {
                if(_player.Location.Y + _player.Height >= platform.Location.Y && 
                    _player.Location.Y - 5 < platform.Location.Y + platform.Height ) 
                {
                    if (_player.Location.X > platform.Location.X + platform.Width) continue;
                    if (_player.Location.X + _player.Width < platform.Location.X) continue;

                    _isJumping = false;
                    _player.Location = new(_player.Location.X, platform.Location.Y - _player.Height);
                    _velocityY = 0f;
                    if (_jumpNext) _velocityY = _jumpHeight;
                    break;
                }

                /*
                if(_player.Location.Y <= platform.Location.Y + platform.Height &&
                    _player.Location.Y + 5 + _player.Height > platform.Location.Y)
                {
                    _player.Location = new(_player.Location.X, platform.Location.Y + platform.Height);
                    _velocityY = 0f;
                    break;
                }
                */
            }


            _velocityY -= _gravity;

            Thread.Sleep(5);
        }
    }
    private void MovePlayer()
    {
        _player.Location = new(_player.Location.X + (int)_velocityX, _player.Location.Y - (int)_velocityY);
    }
    private void KeyDownListener(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Space:
            case Keys.W:
                if (_isJumping) return;
                _jumpNext = true;
                _isJumping = true;
                _velocityY = _jumpHeight;
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
        switch (e.KeyCode)
        {
            case Keys.W:
            case Keys.Space:
                _jumpNext = false;
                break;
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
