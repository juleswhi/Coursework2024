using static System.Math;
using RayEngine.Levels;
using PhysicsEngine;
using System.Diagnostics;
using System.Timers;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using static RayEngine.ShaderType;

namespace RayEngine;

public class Raycaster
{
    private FastLoop _fastLoop;
    private Stopwatch _stopwatch = new();
    private Stopwatch _gunTimer = new();

    private Form _gameForm;
    private PictureBox _gameCanvas;

    private System.Timers.Timer _timer;

    private long _msFrameTime;
    private long _msLastFrame;
    private long _msPerDrawCycle;
    private long _msThisFrame;
    private long _frameTime;

    private Level _level;
    private List<Bitmap> _brickSlices = new();

    Rectangle _player;

    float _playerDeltaX, _playerDeltaY, _playerAngle;

    Vec2 Location = new(100, 100);
    bool _movingForward = false;
    bool _movingDown = false;
    bool _movingLeft = false;
    bool _movingRight = false;

    private bool _isShooting = false;
    private bool _shootLock = true;
    private Bitmap _gun = Materials.gun;

    List<GameObject> _objects = new();
    private SolidBrush _brush = new(Color.Gray);

    SolidBrush brushGreen = new(Color.Green);
    SolidBrush brushRed = new(Color.Red);

    SolidBrush brushDarkGray = new(Color.FromArgb(255, 169, 169, 169));
    SolidBrush brushLightGray = new(Color.FromArgb(255, 240, 240, 240));


    #region Constructors
    public Raycaster(Form form, Level level)
    {
        _gameForm = form;
        InitialiseForm();

        _level = level;

        _player = new(Location.ToPoint(), new(10, 10));

        _playerDeltaX = (float)Math.Cos(_playerAngle) * 5;
        _playerDeltaY = (float)Math.Sin(_playerAngle) * 5;

        CreateMap(level.Map.AsSpan());
        CreateEnemy();

        _fastLoop = new(GameLoop);
        _stopwatch.Start();
        _gunTimer.Start();
    }

    private void InitialiseForm()
    {
        _gameCanvas  = new();
        _gameCanvas.Dock = DockStyle.Fill;
        _gameCanvas.BackColor = Color.Gray;
        _gameForm.Controls.Add(_gameCanvas);
        _gameCanvas.Paint += viewCanvasDraw;

        _gameForm.KeyDown += KeyPressedDown;
        _gameForm.KeyUp += KeyPressedUp;
    }

#endregion

    #region Keybinds

    private void KeyPressedUp(object? sender, KeyEventArgs e)
    {
        switch(e.KeyCode)
        {
            case Keys.S: 
            case Keys.Down:
                _movingDown = false;
            break;
            case Keys.W:
            case Keys.Up:
                _movingForward = false;
                break;
            case Keys.A:
            case Keys.Left:
                _movingLeft = false;
                break;
            case Keys.D:
            case Keys.Right:
                _movingRight = false;
                break;
            default:
                break;
        }
    }

    private void KeyPressedDown(object? sender, KeyEventArgs e)
    {
        switch(e.KeyCode)
        {
            case Keys.S: 
            case Keys.Down:
                Location.X -= _playerDeltaX; Location.Y -= _playerDeltaY;
            break;
            case Keys.W:
            case Keys.Up:
                Location.X += _playerDeltaX; Location.Y += _playerDeltaY;
                break;
            case Keys.A:
            case Keys.Left:
                _playerAngle -= 0.1f;
                if (_playerAngle < 0) _playerAngle += 2 * (float)Math.PI;
                _playerDeltaX = (float)Math.Cos((double)_playerAngle) * 5;
                _playerDeltaY = (float)Math.Sin((double)_playerAngle) * 5;
                break;
            case Keys.D:
            case Keys.Right:
                _playerAngle += 0.1f;
                if (_playerAngle > 2 * (float)Math.PI) _playerAngle -= 2 * (float)Math.PI;
                _playerDeltaX = (float)Math.Cos((double)_playerAngle) * 5;
                _playerDeltaY = (float)Math.Sin((double)_playerAngle) * 5;
                break;
            case Keys.Enter:
            case Keys.Space:
                if(!_shootLock)
                    _isShooting = true;
                break;
            default:
                break;
        }
    }

    #endregion

    #region Loops
    private void Invalidate() { 
        // _canvas.Refresh(); 
         
        _gameCanvas.Refresh(); 
    }

    private void GameLoop(object? sender, ElapsedEventArgs e)
    {
        Move();
        Render();
    }

    private void GameLoop(double elapsedTime)
    {
        Move();
        if (_stopwatch.ElapsedMilliseconds - _frameTime > 1000 / 60)
        {
            Render();
        }
    }

    #endregion

    private void Move()
    {
        if (_movingLeft) Location.X -= 2;
        if (_movingRight) Location.X += 2;
        if (_movingForward) Location.Y -= 2;
        if (_movingDown) Location.Y += 2;

        _player.Location = Location.ToPoint();
    }

    private void CreateMap(ReadOnlySpan<int> map)
    {
        _brickSlices =  DrawTexture();
        float rowSize = (float)Sqrt(map.Length);
        if (rowSize % 1 != 0) return;

        int rectangleSize = ( 400 / 8 );
        for (int i = 0; i < map.Length; i++)
        {
            int x = (i % (int)rowSize) * rectangleSize;
            int y = (i / (int)rowSize) * rectangleSize;

            Rectangle rect = new(x, y, rectangleSize, rectangleSize);
            if (map[i] == 1)
                _objects.Add(new Wall(rect));
            else if (map[i] == 2)
                _objects.Add(new Door(rect));
        }
    }

    private void CreateEnemy()
    {
        Rectangle rect = new(new Point(150, 150), new Size(10, 10));

        _objects.Add(new Enemy(rect));
    }

    private bool ShouldRenderBullet = false;
    private void Render()
    {
        if (_gunTimer.Elapsed.Milliseconds > 500)
        {
            _shootLock = false;
            ShouldRenderBullet = false;
        }

        _frameTime = _stopwatch.ElapsedMilliseconds;
        Invalidate();
        _msPerDrawCycle = _stopwatch.ElapsedMilliseconds - _frameTime;
        _msLastFrame = _msThisFrame;
        _msThisFrame = _stopwatch.ElapsedMilliseconds;
        _msFrameTime = _msThisFrame - _msLastFrame;
    }



    private void viewCanvasDraw(object? sender, PaintEventArgs e)
    {
        e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

        e.Graphics.FillRectangle(brushLightGray, new(75, 0, _gameCanvas.Width - 197, _gameCanvas.Height / 2));
        e.Graphics.FillRectangle(brushDarkGray, new(75, _gameCanvas.Height / 2, _gameCanvas.Width - 197, _gameCanvas.Height / 2));

        List<Ray> rays = new();

        PointF start = new Vec2(_player.Left + (0.5f * _player.Width), _player.Top + (0.5f * _player.Height)).ToPointF();

        for (float i = -0.5f; i < 0.5f; i += 0.005f)
        {
            rays.Add(DrawRay(start, new((float)Math.Cos((double)_playerAngle + i) * 5, (float)Math.Sin((double)_playerAngle + i) * 5)));
        }
        
        if (rays.Count == 0) return;

        float width = _gameCanvas.Width / rays.Count;

        for(int i = 0; i < rays.Count; i++)
        {
            int height = (int)(5000 / rays[i].Distance);
            int intensity = (int)(255 / rays[i].Distance);

            intensity = Math.Clamp(intensity + 100, 0, 255);

            switch (rays[i].Shader)
            {
                case BRICK:
                    e.Graphics.DrawImage(_brickSlices[rays[i].Slice], new Rectangle(i * (int)width + 75, (_gameCanvas.Height / 2) - (int)(0.5 * height), (int)width, height));
                    break;
                case DOOR:
                    _brush.Color = Color.FromArgb(255, intensity, intensity, intensity);
                    e.Graphics.FillRectangle(_brush, new(i * (int)width + 75, (_gameCanvas.Height / 2) - (int)(0.5 * height), (int)width, height));
                    break;
                case ENEMY:
                    _brush.Color = Color.ForestGreen;
                    e.Graphics.FillRectangle(_brush, new(i * (int)width + 75, (_gameCanvas.Height / 2) - (int)(0.5 * height), (int)width, height));
                    break;
                case VOID:
                    break;
            }

        }

        if (_isShooting)
        {
            _isShooting = false;
            _shootLock = true;
            _gunTimer.Restart();
            ShouldRenderBullet = true;
        }

        if(ShouldRenderBullet)
            e.Graphics.FillRectangle(brushGreen, new(new((_gameCanvas.Width / 2 ) - 30, _gameCanvas.Bottom - (int)(_gameCanvas.Height * 0.5)), new Size(20, _gameCanvas.Height)));
        
        e.Graphics.DrawImage(_gun, new Rectangle(new Point(_gameCanvas.Width / 2 - ( 197 / 2), _gameCanvas.Bottom - 100), new Size(150, 150)));

        // e.Graphics.DrawString(_isShooting.ToString() + "\n" + _shootLock.ToString() + "\n" + _gunTimer.Elapsed.Seconds.ToString(), new Font(FontFamily.GenericMonospace, 20), brushGreen, new Point(100, 100));
    }

    private Ray DrawRay(PointF start, Vec2 delta)
    {
        ReadOnlySpan<GameObject> rects = CollectionsMarshal.AsSpan(_objects);
        float max = 95f;

        for(float i = 0; i < max; i += 0.3f)
        {
            var point = new PointF(start.X + (delta.X * i), start.Y + (delta.Y * i));
            for (int j = 0; j < rects.Length; j++)
            {
                // Check if inside or touching a wall
                if (rects[j].Rectangle.X <= point.X && point.X < rects[j].Rectangle.X + rects[j].Rectangle.Width && rects[j].Rectangle.Y < point.Y && point.Y < rects[j].Rectangle.Y + rects[j].Rectangle.Height)
                {
                    float hit = point.X - rects[j].Rectangle.X;
                    int sliceIndex = (int)(hit * _brickSlices.Count) % _brickSlices.Count;
                    return new Ray(i, sliceIndex, rects[j].Shader);
                }
            }
        }

        return new(max, -1, VOID);
    }


    private Bitmap _bricks = Materials.Bricks_001;
    private List<Bitmap> DrawTexture()
    {
        List<Bitmap> images = new();
        for(int i = 0; i < _bricks.Height; i++)
        {
            Rectangle rect = new(i, 0, 1, _bricks.Height);
            Bitmap clone = _bricks.Clone(rect, _bricks.PixelFormat);
            images.Add(clone);
        }

        return images;
    }
}
