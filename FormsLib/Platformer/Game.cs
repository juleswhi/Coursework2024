namespace FormsLib.Platformer;

internal class Game
{
    private float _targetX = 0;
    private float _moveRateX = 0.5f;

    private float _friction = 0.2f;
    private float _gravity = 0.3f;
    private float _jumpHeight = 8f;

    private float _velocityX = 0;
    private float _velocityY = 0;
    private bool _isJumping = false;
    private bool _formClosed = false;

    private bool _movingLeft = false;
    private bool _movingRight = false;

    private bool _jumpNext = false;


    private PictureBox _player;
    private PictureBox _token;
    private List<PictureBox> _platforms = new();

    private Label _labelWin;


    public List<Bitmap>? _playerIdleSprites = new();
    private int _currentIdleIndex = 0;
    private int _currentDrawIndex = 0;

    private Form _form;


    public Game(Form form, Label win)
    {
        foreach (var control in form.Controls)
        {
            if (control is PictureBox)
            {
                if ((control as PictureBox)!.Tag == "Player") _player = control as PictureBox;
                else if ((control as PictureBox)!.Tag == "Token") _token = control as PictureBox;
                else _platforms.Add(control as PictureBox);
            }
        }

        _form = form;

        _labelWin = win;

        form.KeyDown += KeyDownListener;
        form.KeyUp += KeyUpListener;

        Thread gameLoop = new Thread(() =>
        {
            
            Bitmap image = new Bitmap(Properties.Resources.Idle);

            List<Bitmap> images = SpriteManager.GetSpritesFromSheet(image, 8);

            for (int i = 0; i < images.Count; i++)
            {
                images[0] = SpriteManager.Crop(images[i]);
            }

            _playerIdleSprites = images;

            _player.Enabled = true;
            form.Invoke(_player.Show);
            
            while (!_formClosed)
                GameLoop();
        });
        
        gameLoop.Start();

        form.FormClosed += (sender, e) => _formClosed = true;
       
    }

    private void GameLoop()
    {
        if (IsWin())
            // _labelWin.Text = "You Win!";

        // DrawImage();

        _velocityX = _velocityX + (_targetX - _velocityX) * _moveRateX;

        if (_velocityY != 0)
            _velocityX = _velocityX - (_friction * _velocityX);

        foreach (var platform in _platforms)
        {
            // Coming from RHS
            if (_player.Left <= platform.Right + 2 &&
                _player.Right > platform.Left + 2 &&
                _player.Bottom > platform.Top &&
                _player.Top < platform.Bottom)
            {
                if (_movingLeft)
                    _velocityX = 0f;

                _form.Invoke(() => _player.Location = new(platform.Right, _player.Location.Y));
                break;
            }

            // Coming from LHS 
            if (_player.Right >= platform.Left - 2 &&
                _player.Left < platform.Right - 2 &&
                _player.Bottom > platform.Top &&
                _player.Top < platform.Bottom)
            {

                if (_movingRight)
                    _velocityX = 0f;

                _form.Invoke(() => _labelWin.Text = "LEFT");

                _form.Invoke(() => _player.Location = new(platform.Left - _player.Width, _player.Location.Y));
                break;
            }
        }

        _form.Invoke(() =>_player.Location = new(_player.Location.X + (int)_velocityX, _player.Location.Y - (int)_velocityY));

        foreach (var platform in _platforms)
        {
            if (_player.Bottom >= platform.Top &&
                _player.Top < platform.Top + 2 &&
                _player.Right > platform.Left &&
                _player.Left < platform.Right)
            {
                _isJumping = false;
                _form.Invoke(() => _player.Location = new(_player.Location.X, platform.Location.Y - _player.Height));
                _velocityY = 0f;
                if (_jumpNext) _velocityY = _jumpHeight;
                break;
            }

            if (_player.Top <= platform.Bottom &&
                _player.Bottom > platform.Bottom &&
                _player.Right > platform.Left &&
                _player.Left < platform.Right)
            {
                _form.Invoke(() => _player.Location = new(_player.Location.X, platform.Location.Y + platform.Height + 3));
                _velocityY -= 10f;
                break;
            }
        }

        _velocityY -= _gravity;

        Thread.Sleep(5);
    }

    private bool IsWin()
    {
        if(_player.Right >= _token.Left && 
            _player.Left <= _token.Right &&
            _player.Top <= _token.Bottom &&
            _player.Bottom >= _token.Top)
        {
            return true;
        }

        return false;
    }

    private void DrawImage()
    {

        if (_currentDrawIndex != 10) { _currentDrawIndex++; return; }

        _currentDrawIndex = 0;

        if (_velocityX > -0.1f && _velocityX < 0.1f)
        {
            _player.Image = _playerIdleSprites[_currentIdleIndex];
            if (_currentIdleIndex == 7) _currentIdleIndex = 0;
            else _currentIdleIndex++;
        }
        else if (_velocityX > 0.1f)
        {
            // Right
        }
        else if (_velocityX < -0.1f)
        {
            // Left
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
