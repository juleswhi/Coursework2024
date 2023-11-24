using static RayEngine.ShaderType;
using PhysicsEngine;
using RayEngine.Levels;
using System.Diagnostics;
using System.Timers;

namespace RayEngine
{
    public partial class formLevelEditor : Form
    {
        private Stopwatch sw = new();
        private FastLoop fl;
        private PictureBox _canvas;
        private int[] map;
        private SolidBrush red = new(Color.Red);
        private SolidBrush gray = new(Color.Gray);
        private SolidBrush blue = new(Color.Blue);
        private long _msFrameTime;
        private long _msLastFrame;
        private long _msPerDrawCycle;
        private long _msThisFrame;
        private long _frameTime;
        private bool mouseDown = false;
        public formLevelEditor()
        {
            InitializeComponent();
            MouseDown += GetMouseDown;
            MouseUp += GetMouseUp;
            _canvas = new();
            _canvas.Dock = DockStyle.Fill;
            _canvas.BackColor = Color.Gray;
            _canvas.Paint += Draw;
            Controls.Add(_canvas);


            Level l1 = new LevelOne();
            map = l1.Map;

            CreateMap();
            FastLoop fl = new(gameLoop);
            sw.Start();
        }

        private void GetMouseUp(object? sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void GetMouseDown(object? sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void gameLoop(double time)
        {
            if (sw.ElapsedMilliseconds - _frameTime > 1000 / 60)
            {
                _frameTime = sw.ElapsedMilliseconds;
                _canvas.Refresh();
                _msPerDrawCycle = sw.ElapsedMilliseconds - _frameTime;
                _msLastFrame = _msThisFrame;
                _msThisFrame = sw.ElapsedMilliseconds;
                _msFrameTime = _msThisFrame - _msLastFrame;
            }
        }

        List<GameObject> objects = new();

        private void Draw(object? sender, PaintEventArgs e)
        {

            foreach(GameObject gameObject in objects) {
                switch(gameObject.Shader)
                {
                    case DESIGNER_WALL:
                        e.Graphics.FillRectangle(red, gameObject.Rectangle);
                        break;
                    case DESIGNER_SELECTED_WALL:
                        e.Graphics.FillRectangle(blue, gameObject.Rectangle);
                        break;
                }
            }


        }

        private void CreateMap()
        {
            float rowSize = (float)Math.Sqrt(map.Length);
            if (rowSize % 1 != 0) return;

            int rectangleSize = (400 / 8);
            for (int i = 0; i < map.Length; i++)
            {
                int x = (i % (int)rowSize) * rectangleSize;
                int y = (i / (int)rowSize) * rectangleSize;

                Rectangle rect = new(x, y, rectangleSize, rectangleSize);

                if (map[i] == 1)
                    if (rect.IntersectsWith(new(PointToClient(MousePosition), new(1, 1))))
                    {
                        objects.Add(new Wall(rect) { Shader = DESIGNER_SELECTED_WALL });
                    }
                    else
                        objects.Add(new Wall(rect) { Shader = DESIGNER_WALL });
                else if (map[i] == 2)
                    objects.Add(new Door(rect) { Shader = DESIGNER_DOOR });
            }
        }
    }



}
