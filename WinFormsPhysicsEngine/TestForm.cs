using System.Diagnostics;

namespace PhysicsEngine;

public partial class TestForm : Form
{
    private PlatformerRigidbody _player;
    private readonly FastLoop _fastLoop;
    private readonly Stopwatch _stopwatch = new();

    private Physics _physics = new();

    // Physics System
    // Mouse left and mouse right

    private long _msFrameTime;
    private long _msLastFrame;
    private long _msPerDrawCycle;
    private long _msThisFrame;

    Font debugFont = new(FontFamily.GenericMonospace, 10);
    SolidBrush debugBrush = new SolidBrush(Color.WhiteSmoke);

    private long _frameTime;


    public TestForm()
    {
        InitializeComponent();
        KeyDown += TestForm_KeyDown;
        KeyUp += TestForm_KeyUp;
        _fastLoop = new(GameLoop);
        _stopwatch.Start();
        _player = new(_physics);
        _physics.Friction = 1.5f;
    }

    private void TestForm_KeyUp(object? sender, KeyEventArgs e)
    {
        switch(e.KeyCode)
        {
            case Keys.W:
            case Keys.Space:
            case Keys.Up:
                break;
            case Keys.Right:
            case Keys.D:
                _player.IsMovingRight = false;
                break;
            case Keys.A:
            case Keys.Left:
                _player.IsMovingLeft = false;
                break;
            default:
                break;
        }
    }

    private void TestForm_KeyDown(object? sender, KeyEventArgs e)
    {
        switch(e.KeyCode)
        {
            case Keys.W:
            case Keys.Space:
            case Keys.Up:
                _player.Jump();
                break;
            case Keys.Right:
            case Keys.D:
                _player.MoveRight();
                break;
            case Keys.A:
            case Keys.Left:
                _player.MoveLeft();
                break;
            default:
                break;
        }
    }
    
    
    
    private void FormMainWindow_Load(object sender, EventArgs e)
    {
        ObjectTemplates.CreateWall(0, 0, 65, Canvas.Height);
        ObjectTemplates.CreateWall(Canvas.Width - 65, 0, Canvas.Width, Canvas.Height);
        ObjectTemplates.CreateWall(0, 0, Canvas.Width, 65);
        ObjectTemplates.CreateWall(0, Canvas.Height - 65, Canvas.Width, Canvas.Height);


        _player = ObjectTemplates.CreatePlayer(ref _physics, new(200,200));
    }

    private void InvalidateWindow() =>
        Canvas.Refresh();

    private void Canvas_DrawGame(object sender, PaintEventArgs e)
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

        e.Graphics.DrawString(_player.IsJumping.ToString(), debugFont, debugBrush, new PointF(80, 90));
        e.Graphics.DrawString(_player.Object.Velocity.Y.ToString(), debugFont, debugBrush, new PointF(80, 100));

        foreach (var obj in Physics.ListStaticObjects)
            obj.Shader.PreDraw(obj, e.Graphics);
        foreach (var obj in Physics.ListStaticObjects)
            obj.Shader.Draw(obj, e.Graphics);
        foreach (var obj in Physics.ListStaticObjects)
            obj.Shader.PostDraw(obj, e.Graphics);
    }

    private void GameLoop(double elapsedTime)
    {
        RunEngine(elapsedTime);

        // _player.Move();
        if (!(_player.Object!.Velocity.Y < 12.5f && _player.Object.Velocity.Y > 10.5f)) _player.IsJumping = true;
        else _player.IsJumping = false;

        if (_player.IsMovingLeft && _player.IsMovingRight) _physics.Friction = 0f;
        else _physics.Friction = 3f;
       
        if (_stopwatch.ElapsedMilliseconds - _frameTime > 1000 / 60)
        {
            Render();
        }
    }

    private void Render()
    {
        _frameTime = _stopwatch.ElapsedMilliseconds;
        InvalidateWindow();
        _msPerDrawCycle = _stopwatch.ElapsedMilliseconds - _frameTime;
        _msLastFrame = _msThisFrame;
        _msThisFrame = _stopwatch.ElapsedMilliseconds;
        _msFrameTime = _msThisFrame - _msLastFrame;
    }

    private void RunEngine(double elapsedTime)
    {
        _physics.Tick(elapsedTime);
    }

}
