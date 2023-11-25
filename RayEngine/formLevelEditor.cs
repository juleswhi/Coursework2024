using static RayEngine.ShaderType;
using PhysicsEngine;
using RayEngine.Levels;
using System.Diagnostics;
using System.Timers;
using System.Runtime.Serialization.Json;
using System.Drawing.Text;

namespace RayEngine
{
    public partial class formLevelEditor : Form
    {
        private Button _saveButton;
        private Button _brickButton;
        private Button _floorButton;
        private Button _doorButton;
        private Button _enemyButton;
        private Button _envButton;
        private Label _debugLabel;

        private Stopwatch _stopwatch = new();
        private FastLoop? _fastLoop;
        private PictureBox _canvas;
        private LevelData _level;
        private SolidBrush _redBrush = new(Color.Red);
        private SolidBrush _grayBrush = new(Color.Gray);
        private SolidBrush _blueBrush = new(Color.Blue);
        private SolidBrush _lightBlueBrush = new(Color.LightBlue);
        private long _msFrameTime;
        private long _msLastFrame;
        private long _msPerDrawCycle;
        private long _msThisFrame;
        private long _frameTime;
        List<GameObject> _objects = new();
        List<GameObject> _environment => _objects.Where(x => x.Shader != ShaderType.ENEMY || x.Shader != ShaderType.DESIGNER_ENEMY || x.Shader != ShaderType.DESIGNER_SELECTED_ENEMY).ToList();
        List<GameObject> _enemies => _objects.Where(x => x.Shader == ShaderType.ENEMY || x.Shader == ShaderType.DESIGNER_ENEMY || x.Shader == ShaderType.DESIGNER_SELECTED_ENEMY).ToList();

        private SelectMode _mode = SelectMode.Environment;

        enum SelectMode
        {
            Environment,
            Enemy
        }

        public formLevelEditor()
        {
            InitializeComponent();

            _canvas = new();
            _canvas.Size = new(450, 450);
            _canvas.BackColor = Color.Gray;
            _canvas.Paint += Draw;
            Controls.Add(_canvas);
            _canvas.MouseDown += GetMouseDown;

            _level = MapSerializer.Deserialize("../../../LevelOne.txt")!;

            _saveButton = new();
            _saveButton.Text = "Save Level";
            _saveButton.Click += SaveButton_Click;
            _saveButton.Location = new(600, 50);
            Controls.Add(_saveButton);

            _brickButton = new();
            _brickButton.Text = "Brick";
            _brickButton.Click += ChangeMaterial;
            _brickButton.Location = new(600, 100);
            Controls.Add(_brickButton);

            _floorButton = new();
            _floorButton.Text = "Floor";
            _floorButton.Click += ChangeMaterial;
            _floorButton.Location = new(600, 125);
            Controls.Add(_floorButton);

            _doorButton = new();
            _doorButton.Text = "Door";
            _doorButton.Click += ChangeMaterial;
            _doorButton.Location = new(600, 150);
            Controls.Add(_doorButton);

            _enemyButton = new();
            _enemyButton.Text = "Enemy Selection";
            _enemyButton.Click += _enemyButton_Click;
            _enemyButton.Location = new(600, 250);
            Controls.Add(_enemyButton);

            _envButton = new();
            _envButton.Text = "Env Selection";
            _envButton.Click += _enemyButton_Click;
            _envButton.Location = new(600, 275);
            Controls.Add(_envButton);

            _debugLabel = new();
            _debugLabel.Text = "";
            _debugLabel.Location = new(600, 300);
            Controls.Add(_debugLabel);

            CreateMap();
            _fastLoop = new(gameLoop);
            _stopwatch.Start();
        }


        private void _enemyButton_Click(object? sender, EventArgs e)
        {
            if (_mode == SelectMode.Enemy)
                _mode = SelectMode.Environment;
            else _mode = SelectMode.Enemy;
        }

        private void ChangeMaterial(object? sender, EventArgs e)
        {
            if(_mode == SelectMode.Enemy)
            {
                _level.Enemies.Add(PointToClient(MousePosition));
                CreateMap();
                return;
            }


            int materialNumber = 0;

            switch((sender as Button)!.Text.ToLower())
            {
                case "brick":
                    materialNumber = 1;
                    break;
                case "floor":
                    materialNumber = 0;
                    break;
                case "door":
                    materialNumber = 2;
                    break;
            }

            foreach(var obj in _objects)
            {
                if (!obj.Shader.ToString().Contains("SELECTED")) continue;

                _level.Map[obj.MapIndex] = materialNumber;
            }

            CreateMap();
        }

        private void SaveButton_Click(object? sender, EventArgs e)
        {
            _level.Serialize("../../../LevelOne.txt");
            _saveButton.Text = "Saved!";
        }

        private void GetMouseDown(object? sender, MouseEventArgs e)
        {
            foreach (var obj in _objects)
            {
                if (!obj.Rectangle.IntersectsWith(new(PointToClient(MousePosition), new(1, 1)))) continue;

                switch (obj.Shader)
                {
                    case DESIGNER_WALL:
                        obj.Shader = DESIGNER_SELECTED_WALL;
                        break;
                    case DESIGNER_SELECTED_WALL:
                        obj.Shader = DESIGNER_WALL;
                        break;
                    case DESIGNER_DOOR:
                        obj.Shader = DESIGNER_SELECTED_DOOR;
                        break;
                    case DESIGNER_SELECTED_DOOR:
                        obj.Shader = DESIGNER_DOOR;
                        break;
                    case DESIGNER_ENEMY:
                        obj.Shader = DESIGNER_SELECTED_ENEMY;
                        break;
                    case DESIGNER_SELECTED_ENEMY:
                        obj.Shader = DESIGNER_ENEMY;
                        break;
                    case DESIGNER_FLOOR:
                        obj.Shader = DESIGNER_SELECTED_FLOOR;
                        break;
                    case DESIGNER_SELECTED_FLOOR:
                        obj.Shader = DESIGNER_FLOOR;
                        break;
                }
            }
        }

        private void gameLoop(double time)
        {
            _debugLabel.Text = _mode.ToString();
            if (_stopwatch.ElapsedMilliseconds - _frameTime > 1000 / 60)
            {
                _frameTime = _stopwatch.ElapsedMilliseconds;
                _canvas.Refresh();
                _msPerDrawCycle = _stopwatch.ElapsedMilliseconds - _frameTime;
                _msLastFrame = _msThisFrame;
                _msThisFrame = _stopwatch.ElapsedMilliseconds;
                _msFrameTime = _msThisFrame - _msLastFrame;
            }
        }


        Dictionary<ShaderType, Brush> shaderLookup = new()
        {
            { DESIGNER_WALL, new SolidBrush(Color.Red) },
            { DESIGNER_SELECTED_WALL, new SolidBrush(Color.Blue) },
            { DESIGNER_DOOR, new SolidBrush(Color.Green) },
            { DESIGNER_SELECTED_DOOR, new SolidBrush(Color.Blue) },
            { DESIGNER_FLOOR, new SolidBrush(Color.Gray) },
            { DESIGNER_SELECTED_FLOOR, new SolidBrush(Color.Blue) },
            { VOID, new SolidBrush(Color.Transparent) },
            { ENEMY, new SolidBrush(Color.Black) },
            { DESIGNER_ENEMY, new SolidBrush(Color.Black) },
            { DESIGNER_SELECTED_ENEMY, new SolidBrush(Color.Blue) }
        };

        private void Draw(object? sender, PaintEventArgs e)
        {
            foreach (GameObject gameObject in _environment)
            {
                if (gameObject.Rectangle.IntersectsWith(new(PointToClient(MousePosition), new(1, 1))))
                    e.Graphics.FillRectangle(_lightBlueBrush, gameObject.Rectangle);
                else e.Graphics.FillRectangle(shaderLookup[gameObject.Shader], gameObject.Rectangle);
            }

            foreach(GameObject enemy in _enemies)
            {
                if (enemy.Rectangle.IntersectsWith(new(PointToClient(MousePosition), new(1, 1))))
                    e.Graphics.FillRectangle(_lightBlueBrush, enemy.Rectangle);
                else e.Graphics.FillRectangle(shaderLookup[enemy.Shader], enemy.Rectangle);
            }

            // e.Graphics.FillRectangle(red, new(PointToClient(MousePosition), new Size(20, 20)));
        }

        private void CreateMap()
        {
            _objects.Clear();
            float rowSize = (float)Math.Sqrt(_level.Map.Length);
            if (rowSize % 1 != 0) return;

            int rectangleSize = (400 / 8);

            for (int i = 0; i < _level.Map.Length; i++)
            {
                int x = (i % (int)rowSize) * rectangleSize;
                int y = (i / (int)rowSize) * rectangleSize;

                Rectangle rect = new(x, y, rectangleSize, rectangleSize);

                if (_level.Map[i] == 0)
                    _objects.Add(new Floor(rect) { Shader = DESIGNER_FLOOR, MapIndex = i });
                if (_level.Map[i] == 1)
                    _objects.Add(new Wall(rect) { Shader = DESIGNER_WALL, MapIndex = i});
                else if (_level.Map[i] == 2)
                    _objects.Add(new Door(rect) { Shader = DESIGNER_DOOR, MapIndex = i});
            }

            for(int i = 0; i < _level.Enemies.Count; i++)
            {
                _objects.Add(new Enemy(new(_level.Enemies[i], new(20, 20))));
            }

        }
    }
}
