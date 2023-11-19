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

    private System.Timers.Timer _timer;

    private long _msFrameTime;
    private long _msLastFrame;
    private long _msPerDrawCycle;
    private long _msThisFrame;
    private long _frameTime;

    private Level _level;

    Rectangle _player;

    float _playerDeltaX, _playerDeltaY, _playerAngle;

    Vec2 Location = new(100, 100);
    bool _movingForward = false;
    bool _movingDown = false;
    bool _movingLeft = false;
    bool _movingRight = false;



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

        _fastLoop = new(GameLoop);
        _stopwatch.Start();
    }

    private void InitialiseForm()
    {
        PictureBox canvas = new();
        canvas.Dock = DockStyle.Fill;
        canvas.BackColor = Color.Gray;
        _form.Controls.Add(canvas);
        _canvas = canvas;
        // Events
        _form.KeyDown += KeyPressedDown;
        _form.KeyUp += KeyPressedUp;
        _canvas.Paint += Draw;
    }

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


    private void Invalidate() => _canvas.Refresh();

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
    private void Move()
    {
        if (_movingLeft) Location.X -= 2;
        if (_movingRight) Location.X += 2;
        if (_movingForward) Location.Y -= 2;
        if (_movingDown) Location.Y += 2;

        _player.Location = Location.ToPoint();
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

    List<Rectangle> walls = new();

    private void Draw(object? sender, PaintEventArgs e)
    {
        e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

        ReadOnlySpan<int> map = _level.Map.AsSpan();

        int rectangleSize = ( 400 / 8 ) - 5;

        for (int i = 0; i < 64; i++)
        {
            int x = (i % 8) * rectangleSize;
            int y = (i / 8) * rectangleSize;

            Rectangle rect = new(x, y, rectangleSize - 2, rectangleSize - 2);
            if (map[i] == 1) { 
                e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), rect);
                walls.Add(rect);
            }
            else
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), rect);
        }

        e.Graphics.FillRectangle(new SolidBrush(Color.Red), _player);
        Pen directionPen = new Pen(Color.White);
        PointF startPoint = new Vec2(_player.Left + ( 0.5f * _player.Width), _player.Top + ( 0.5f * _player.Height)).ToPointF();
        PointF endPoint = new PointF(startPoint.X + (_playerDeltaX * 5), startPoint.Y + ( 5 * _playerDeltaY));
        e.Graphics.DrawLine(directionPen, startPoint, endPoint);


        for(var i = -0.5f; i < 0.5f; i += 0.1f)
        {
            DrawRay(e.Graphics, startPoint, new((float)Math.Cos((double)_playerAngle + i) * 5, (float)Math.Sin((double)_playerAngle + i) * 5));
        }

        e.Graphics.DrawString($"Player Delta: {_playerDeltaX}\nNew Delta: {(float)Math.Cos((double)_playerAngle + 1) * 5}\nAngle: {_playerAngle}\n", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.Orange), new Point(400, 300));
    }

    Pen green = new(Color.Green);
    Pen red = new(Color.Red);

    private float DrawRay(Graphics g, PointF start, Vec2 delta)
    {
        ReadOnlySpan<Rectangle> rects = CollectionsMarshal.AsSpan(walls);
        float max = 50f;
        // Algorithm for mitigating amount of checks
        // DDA?

        for(float i = 0; i < max; i += 0.5f)
        {
            var point = new PointF(start.X + (delta.X * i), start.Y + (delta.Y * i));
            for (int j = 0; j < rects.Length; j++)
            {
                // Check if inside or touching a wall
                if (rects[j].X <= point.X && point.X < rects[j].X + rects[j].Width && rects[j].Y < point.Y && point.Y < rects[j].Y + walls[j].Height)
                {
                    // Draw line to the wall
                    g.DrawLine(red, start, point);
                    return 0f;
                }
            }
        }

        g.DrawLine(green, start, new PointF(start.X + (delta.X * max), start.Y + (delta.Y * max)));
        return max;
    }

       
}
