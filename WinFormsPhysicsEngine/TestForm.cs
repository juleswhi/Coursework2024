using System.Diagnostics;

namespace PhysicsEngine;

public partial class TestForm : Form
{
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
        _fastLoop = new(GameLoop);
        _stopwatch.Start();
    }

    private void FormMainWindow_Load(object sender, EventArgs e)
    {
        ObjectTemplates.CreateWall(0, 0, 65, Canvas.Height);
        ObjectTemplates.CreateWall(Canvas.Width - 65, 0, Canvas.Width, Canvas.Height);
        ObjectTemplates.CreateWall(0, 0, Canvas.Width, 65);
        ObjectTemplates.CreateWall(0, Canvas.Height - 65, Canvas.Width, Canvas.Height);

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

        e.Graphics.DrawString("Hello, World!", debugFont, debugBrush, new PointF(80, 90));

        foreach (var obj in Physics.ListStaticObjects)
            obj.Shader.PreDraw(obj, e.Graphics);
        foreach (var obj in Physics.ListStaticObjects)
            obj.Shader.Draw(obj, e.Graphics);
        foreach (var obj in Physics.ListStaticObjects)
            obj.Shader.PostDraw(obj, e.Graphics);
    }


    private void GameLoop(double elapsedTime)
    {
        // RunEngine ( elapsed time )
        RunEngine(elapsedTime);

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
