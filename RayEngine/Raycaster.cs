#define DEBUG
using static System.Math;
using RayEngine.Levels;
using PhysicsEngine;
using System.Diagnostics;
using System.Timers;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

namespace RayEngine;

public class Raycaster
{
    private Form _form;
    private PictureBox _canvas;
    private FastLoop _fastLoop;
    private Stopwatch _stopwatch = new();

    private formView _view;
    private PictureBox _viewCanvas;

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

    private Bitmap _gun = Materials.gun;

    List<(Rectangle, Color)> _walls = new();
    private SolidBrush _brush = new(Color.Gray);
    Pen penGreen = new(Color.Green);
    Pen penRed = new(Color.Red);

    SolidBrush brushGreen = new(Color.Green);
    SolidBrush brushRed = new(Color.Red);

    SolidBrush brushDarkGray = new(Color.FromArgb(255, 169, 169, 169));
    SolidBrush brushLightGray = new(Color.FromArgb(255, 240, 240, 240));


    #region Constructors
    public Raycaster(Form form, Level level)
    {
        #pragma warning disable
        _form = form;
        InitialiseForm();
#pragma warning enable

        _level = level;

        _player = new(Location.ToPoint(), new(10, 10));

        _playerDeltaX = (float)Math.Cos(_playerAngle) * 5;
        _playerDeltaY = (float)Math.Sin(_playerAngle) * 5;

        // Timer
        //_timer = new(1);
        //_timer.Elapsed += GameLoop;
        //_timer.Start();

        CreateMap(level.Map.AsSpan());

        _fastLoop = new(GameLoop);
        _stopwatch.Start();
    }

    private void InitialiseForm()
    {

#if Debug
        formView formView = new();
        PictureBox canvas = new();
        canvas.Dock = DockStyle.Fill;
        canvas.BackColor = Color.Gray;
        formView.Controls.Add(canvas);
        _canvas = canvas;
        _canvas.Paint += Draw;
        formView.Show();
#endif


        PictureBox viewCanvas = new();
        viewCanvas.Dock = DockStyle.Fill;
        viewCanvas.BackColor = Color.Gray;
        _form.Controls.Add(viewCanvas);
        viewCanvas.Paint += viewCanvasDraw;
        _viewCanvas = viewCanvas;

        _form.KeyDown += KeyPressedDown;
        _form.KeyUp += KeyPressedUp;
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
            default:
                break;
        }
    }

    #endregion

    #region Loops
    private void Invalidate() { 
        // _canvas.Refresh(); 
         
        _viewCanvas.Refresh(); 
    }

    private void GameLoop(object? sender, ElapsedEventArgs e)
    {
        Move();
        Render();
    }

    private void GameLoop(double elapsedTime)
    {
        Move();
        if(_stopwatch.ElapsedMilliseconds - _frameTime > 1000 / 60)
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
                _walls.Add((rect, Color.Gray));
            else if (map[i] == 2)
                _walls.Add((rect, Color.Green));
        }
    }

    private void Render()
    {
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


        e.Graphics.FillRectangle(brushLightGray, new(75, 0, _viewCanvas.Width - 197, _viewCanvas.Height / 2));
        e.Graphics.FillRectangle(brushDarkGray, new(75, _viewCanvas.Height / 2, _viewCanvas.Width - 197, _viewCanvas.Height / 2));

        List<(float, Color)> distances = new();

        PointF start = new Vec2(_player.Left + (0.5f * _player.Width), _player.Top + (0.5f * _player.Height)).ToPointF();

        for (float i = -0.5f; i < 0.5f; i += 0.005f)
        {
            distances.Add(DrawRay(start, new((float)Math.Cos((double)_playerAngle + i) * 5, (float)Math.Sin((double)_playerAngle + i) * 5)));
        }
        
        if (distances.Count == 0) return;

        float width = _viewCanvas.Width / distances.Count;

        for(int i = 0; i < distances.Count; i++)
        {
            int height = (int)(5000 / distances[i].Item1);
            int intensity = (int)(255 / distances[i].Item1);

            intensity = Math.Clamp(intensity + 100, 0, 255);

            if (distances[i].Item2 == Color.Green)
            {
                _brush.Color = Color.FromArgb(255, 0, intensity, 0);
            }

            else if (distances[i].Item2 == Color.Gray)
            {
                _brush.Color = Color.FromArgb(255, intensity, intensity, intensity);
            }

            // e.Graphics.FillRectangle(_brush, new(i * (int)width + 75, ( _viewCanvas.Height / 2 ) - (int)( 0.5 * height ), (int)width, height));
            e.Graphics.DrawImage(_brickSlices[i], new Rectangle(i * (int)width + 75, (_viewCanvas.Height / 2) - (int)(0.5 * height), (int)width, height));
        }

        
        
        e.Graphics.DrawImage(_gun, new Rectangle(new Point(_viewCanvas.Width / 2 - ( 197 / 2), _viewCanvas.Bottom - 100), new Size(150, 150)));
    }

    private (float, Color) DrawRay(PointF start, Vec2 delta)
    {
        ReadOnlySpan<(Rectangle, Color)> rects = CollectionsMarshal.AsSpan(_walls);
        float max = 95f;

        for(float i = 0; i < max; i += 0.5f)
        {
            var point = new PointF(start.X + (delta.X * i), start.Y + (delta.Y * i));
            for (int j = 0; j < rects.Length; j++)
            {
                // Check if inside or touching a wall
                if (rects[j].Item1.X <= point.X && point.X < rects[j].Item1.X + rects[j].Item1.Width && rects[j].Item1.Y < point.Y && point.Y < rects[j].Item1.Y + rects[j].Item1.Height)
                {
                    return (i, rects[j].Item2);
                }
            }
        }

        return (max, Color.Gray);
    }

    private List<(float, Color)> bullets = new();

    private void Fire()
    {
        bullets.Add(DrawRay(new Vec2(_player.Left + (0.5f * _player.Width), _player.Top + (0.5f * _player.Height)).ToPointF(), new(_playerDeltaX, _playerDeltaY)));
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
