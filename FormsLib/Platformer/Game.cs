using FormsLib.Chess;
using System.Timers;
using System.Windows.Forms;

namespace FormsLib.Platformer;

internal class Game
{

    // Time Delta Time

    private DateTime _lastFrameTime;
    private double _deltaTime;
    private const int _deltaTimeMultiplier = 62;

    private Size _playerSize;
    private float _targetX = 0;

    private const float _moveRateX = 0.5f;
    private const float _friction = 0.2f;
    private const float _gravity = 0.3f;
    private const float _jumpHeight = 8f;

    private float _velocityX = 0;
    private float _velocityY = 0;
    private bool _isJumping = false;
    private bool _formClosed = false;

    private bool _movingLeft = false;
    private bool _movingRight = false;

    private int _animationDirection = 1;

    private bool _jumpNext = false;

    private PictureBox _player;
    private PictureBox _token;
    private List<PictureBox> _platforms = new();
    private List<PictureBox> _buttons = new();

    private Label _labelWin;

    private List<Bitmap>? _playerIdleSprites = new();
    private List<Bitmap>? _playerRunSprites = new();

    private int _currentIdleIndex = 0;
    private int _currentRunIndex = 0;

    private int _currentPlayerRotation = 1;

    private Form _form;

    private bool _readInImages = false;


    public Game(Form form, Label win)
    {
        foreach (var control in form.Controls)
        {
            if (control is PictureBox)
            {
                #pragma warning disable 
                if ((control as PictureBox)!.Tag == "Player") _player = control as PictureBox;
                else if ((control as PictureBox)!.Tag == "Token") _token = control as PictureBox;
                // else if ((control as PictureBox)!.Tag.ToString().Contains("Play")) _buttons.Add(control as PictureBox);
                else _platforms.Add(control as PictureBox);
                #pragma warning enable
            }
        }

        _form = form;

        _labelWin = win;

        _playerSize = _player.Size;

        form.KeyDown += KeyDownListener;
        form.KeyUp += KeyUpListener;

        _lastFrameTime = DateTime.Now;

        Thread gameLoop = new Thread(() =>
        {
            _currentIdleIndex = 0;
            Bitmap idleImage = new Bitmap(Properties.Resources.Idle);
            Bitmap runImage = new Bitmap(Properties.Resources.Run);

            List<Bitmap> idleImages = SpriteManager.GetSpritesFromSheet(idleImage, 8);
            List<Bitmap> runImages = SpriteManager.GetSpritesFromSheet(runImage, 8);

            for (int i = 0; i < idleImages.Count; i++)
            {
                idleImages[i] = SpriteManager.Crop(idleImages[i]);
                runImages[i] = SpriteManager.Crop(runImages[i]);
            }

            _playerIdleSprites = idleImages;
            _playerRunSprites = runImages;

            _form.Invoke(() => _player.Enabled = true);
            _form.Invoke(() => _player.Visible = true);

            _readInImages = true;

            System.Timers.Timer timer = new(5);

            timer.Elapsed += (sender, e) =>
            {
                if (_formClosed) timer.Stop();

                GameLoop();
            };

            timer.Start();
            
        });

        Thread playerSprite = new Thread(() =>
        {
            PictureBox playerSprite = new();

            playerSprite.Size = _player.Size;
            playerSprite.Location = _player.Location;
            playerSprite.BackColor = Color.Green;
            playerSprite.BringToFront();


            _form.Invoke(() => _form.Controls.Add(playerSprite));
            _form.Invoke(() => playerSprite.Show());


            System.Timers.Timer timer = new(100);
            timer.Elapsed += (object sender, ElapsedEventArgs e) => _form.Invoke(() => playerSprite.Location = _player.Location);

            timer.Start();

            
        });


       
        
        gameLoop.Start();

        // playerSprite.Start();

        form.FormClosed += (sender, e) => _formClosed = true;
    }

    private void PressedButton()
    {
        foreach (PictureBox button in _buttons) {
            if (!IsColliding(button)) continue;

            string[] tag = button.Tag.ToString().Split("-");

            switch(tag[1])
            {
                case "Chess":
                    new formBoard().Show();
                    break;
            }
        }

    }

    private bool IsColliding(PictureBox colliding) =>
        _player.Bounds.IntersectsWith(new(colliding.Location, new(colliding.Width, colliding.Height)));

    private Dictionary<Control, Action> _controlActions = new();

    private void AddControlAction(Control control, Action action)
    {
         if(_controlActions.ContainsKey(control))
             _controlActions[control] += action;

         else 
            _controlActions.Add(control, action);
    }

    private bool _gravityEnabled;
    private void GameLoop()
    {

        bool touchingGround = false;

        DateTime currentFrameTime = DateTime.Now;
        TimeSpan elapsed = currentFrameTime - _lastFrameTime;
        _lastFrameTime = currentFrameTime;
        _deltaTime = elapsed.TotalSeconds;


        // if (IsWin())
        // _form.Invoke(() => _labelWin.Text = "You Win!");
        // AddControlAction(_labelWin, () => _labelWin.Text = "You win!");

        _velocityX = _velocityX + (_targetX - _velocityX) * _moveRateX * ( _deltaTimeMultiplier * (float)_deltaTime );

        // PressedButton();


        if(!_movingLeft && !_movingRight)
            _velocityX = _velocityX - (_friction * _velocityX);



        foreach (var platform in _platforms) {

            Rectangle groundBuffer = new Rectangle(
                platform.Left + 2,
                platform.Top + 2,
                platform.Width,
                platform.Height
                );

            if (_player.Bounds.IntersectsWith(groundBuffer))
            {
                _form.Invoke(() => _labelWin.Text = "Touching");

                // If the character is above the surface, move them to the surface
                if (_player.Bottom >= groundBuffer.Top)
                {
                    _isJumping = false;
                    AddControlAction(_player, () => _player.Top = groundBuffer.Top - _player.Height);
                    _velocityY = 0;
                    _gravityEnabled = false;
                }

                // If the character is below the surface, move them to the surface
                if (_player.Bottom > groundBuffer.Bottom)
                {
                    _player.Top = platform.Bottom;
                }

                break;
            }
        }




        AddControlAction(_player, MovePlayer);

        foreach(var controlAction in _controlActions)
        {
            _form.Invoke(controlAction.Value);
        }

        _velocityY -= _gravity * (_deltaTimeMultiplier * (float)_deltaTime);
        _gravityEnabled = true;

        _controlActions.Clear();
    }

    private bool IsWin()
    {
        if(_token is not null)
        if(_player.Right >= _token.Left && 
            _player.Left <= _token.Right &&
            _player.Top <= _token.Bottom &&
            _player.Bottom >= _token.Top)
        {
            return true;
        }

        return false;
    }

    public void Dispose()
    {
        Environment.Exit(0);
    }


    private void MovePlayer() =>
        _player.Location = new Point(
            (int)(_player.Location.X + _velocityX * (_deltaTimeMultiplier * (float)_deltaTime)),
            (int)(_player.Location.Y - _velocityY * (_deltaTimeMultiplier * (float)_deltaTime))
        );

    private void KeyDownListener(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Up:
            case Keys.Space:
            case Keys.W:
                if (_isJumping) return;
                _jumpNext = true;
                _isJumping = true;
                _velocityY = _jumpHeight;
                break;
            case Keys.Left:
            case Keys.A:
                _movingLeft = true;
                _targetX = -10f;
                break;
            case Keys.Right:
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
            case Keys.Up:
            case Keys.W:
            case Keys.Space:
                _jumpNext = false;
                break;
            case Keys.Left:
            case Keys.A:
                if (_movingRight) _targetX = 10f;
                else _targetX = 0f;
                _movingLeft = false;
                break;
            case Keys.Right:
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
