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
        private Button _removeEnemiesButton;
        private Button _resizeButton;

        private TextBox _txtBoxHeight;
        private TextBox _txtBoxWidth;

        private Label _debugLabel;
        private Label _sizeLabel;

        private Stopwatch _stopwatch = new();
        private FastLoop? _fastLoop;
        private PictureBox _canvas;
        private LevelData _level;
        private SolidBrush _redBrush = new(Color.Red);
        private SolidBrush _grayBrush = new(Color.Gray);
        private SolidBrush _blueBrush = new(Color.Blue);
        private SolidBrush _lightBlueBrush = new(Color.LightBlue);
        private SolidBrush _darkBlueBrush = new(Color.DarkBlue);
        private long _msFrameTime;
        private long _msLastFrame;
        private long _msPerDrawCycle;
        private long _msThisFrame;
        private long _frameTime;
        List<GameObject> _objects = new();
        List<GameObject> _environment => _objects.Where(x => x.Shader != ShaderType.ENEMY || x.Shader != ShaderType.DESIGNER_ENEMY || x.Shader != ShaderType.DESIGNER_SELECTED_ENEMY).ToList();
        List<GameObject> _enemies => _objects.Where(x => x.Shader == ShaderType.ENEMY || x.Shader == ShaderType.DESIGNER_ENEMY || x.Shader == ShaderType.DESIGNER_SELECTED_ENEMY).ToList();
        private Point _mosPos => new Point(MousePosition.X - 5, MousePosition.Y - 5);

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
            _saveButton.Location = new(500, 50);
            Controls.Add(_saveButton);

            _brickButton = new();
            _brickButton.Text = "Brick";
            _brickButton.Click += ChangeMaterial;
            _brickButton.Location = new(500, 100);
            Controls.Add(_brickButton);

            _floorButton = new();
            _floorButton.Text = "Floor";
            _floorButton.Click += ChangeMaterial;
            _floorButton.Location = new(500, 125);
            Controls.Add(_floorButton);

            _doorButton = new();
            _doorButton.Text = "Door";
            _doorButton.Click += ChangeMaterial;
            _doorButton.Location = new(500, 150);
            Controls.Add(_doorButton);

            _enemyButton = new();
            _enemyButton.Text = "Selection";
            _enemyButton.Click += _enemyButton_Click;
            _enemyButton.Location = new(500, 250);
            Controls.Add(_enemyButton);

            _debugLabel = new();
            _debugLabel.Text = "";
            _debugLabel.Location = new(500, 275);
            Controls.Add(_debugLabel);

            _sizeLabel = new();
            _sizeLabel.Text = "";
            _sizeLabel.Location = new(600, 125);
            Controls.Add(_sizeLabel);

            _removeEnemiesButton = new();
            _removeEnemiesButton.Text = "Remove";
            _removeEnemiesButton.Click += _removeEnemiesButton_Click;
            _removeEnemiesButton.Location = new(500, 300);
            Controls.Add(_removeEnemiesButton);

            _txtBoxHeight = new();
            _txtBoxWidth = new();
            _txtBoxWidth.Location = new(600, 50);
            _txtBoxHeight.Location = new(600, 75);
            Controls.Add(_txtBoxWidth);
            Controls.Add(_txtBoxHeight);

            _resizeButton = new();
            _resizeButton.Text = "Resize";
            _resizeButton.Click += _resizeButton_Click;
            _resizeButton.Location = new(600, 100);
            Controls.Add(_resizeButton);


            CreateMap();

            _fastLoop = new(gameLoop);
            _stopwatch.Start();
        }

        private void _resizeButton_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(_txtBoxWidth.Text, out int x)) return;
            if (!int.TryParse(_txtBoxHeight.Text, out int y)) return;

            ResizeMap(x, y);
        }

        private void _removeEnemiesButton_Click(object? sender, EventArgs e)
        {
            foreach (var obj in _enemies)
            {
                if (!obj.Shader.ToString().Contains("SELECTED")) continue;
                _level.Enemies.Remove(obj.Rectangle.Location);

            }

            CreateMap();
        }

        private void _enemyButton_Click(object? sender, EventArgs e)
        {
            if (_mode == SelectMode.Enemy)
                _mode = SelectMode.Environment;
            else _mode = SelectMode.Enemy;
        }

        private void ChangeMaterial(object? sender, EventArgs e)
        {
            if (_mode == SelectMode.Enemy) return;
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

                _level.Map[obj.MapIndex.Item1, obj.MapIndex.Item2] = materialNumber;
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
            if (_mode == SelectMode.Enemy)
            {

                foreach (var enemy in _enemies)
                {
                    if (!enemy.Rectangle.IntersectsWith(new(PointToClient(_mosPos), new(1, 1)))) continue; 

                    if(enemy.Shader == ENEMY || enemy.Shader == DESIGNER_ENEMY)
                    {
                        enemy.Shader = DESIGNER_SELECTED_ENEMY;
                    }
                    else if(enemy.Shader == DESIGNER_SELECTED_ENEMY)
                    {
                        enemy.Shader = DESIGNER_ENEMY;
                    }

                    return;
                }

                _level.Enemies.Add(PointToClient(_mosPos));
                CreateMap();
                return;
            }



            foreach (var obj in _objects)
            {
                if (!obj.Rectangle.IntersectsWith(new(PointToClient(_mosPos), new(1, 1)))) continue;
                

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
            _sizeLabel.Text = $"{_level.MapHeight}, {_level.MapWidth}";
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
            { ENEMY, new SolidBrush(Color.Green) },
            { DESIGNER_ENEMY, new SolidBrush(Color.Green) },
            { DESIGNER_SELECTED_ENEMY, new SolidBrush(Color.DarkBlue) }
        };

        private void Draw(object? sender, PaintEventArgs e)
        {
            foreach (GameObject gameObject in _environment)
            {
                if (gameObject.Rectangle.IntersectsWith(new(PointToClient(_mosPos), new(1, 1))))
                {
                    if (_mode == SelectMode.Environment)
                        e.Graphics.FillRectangle(_lightBlueBrush, gameObject.Rectangle);
                }
                else e.Graphics.FillRectangle(shaderLookup[gameObject.Shader], gameObject.Rectangle);
            }

            foreach(GameObject enemy in _enemies)
            {
                if (enemy.Rectangle.IntersectsWith(new(PointToClient(_mosPos), new(1, 1))))
                    e.Graphics.FillRectangle(_darkBlueBrush, enemy.Rectangle);
                else e.Graphics.FillRectangle(shaderLookup[enemy.Shader], enemy.Rectangle);
            }

            if(_mode == SelectMode.Enemy)
            {
                e.Graphics.FillRectangle(_lightBlueBrush, new(PointToClient(_mosPos), new Size(20, 20)));
            }

        }

        private void CreateMap()
        {
            _objects.Clear();
            // if (_level.Map.Length != _level.MapHeight * _level.MapWidth) return;

            int rectangleWidth = 450 / _level.MapWidth;
            int rectangleHeight = 450 / _level.MapHeight;

            for (int i = 0; i < _level.MapHeight; i++)
            {
                for(int j = 0; j < _level.MapWidth; j++)
                {
                    int x = j * rectangleWidth;
                    int y = i * rectangleHeight;

                    Rectangle rect = new(x, y, rectangleWidth, rectangleHeight);

                    if (_level.Map[i, j] == 0)
                        _objects.Add(new Floor(rect) { Shader = DESIGNER_FLOOR, MapIndex = (i, j) });
                    if (_level.Map[i, j] == 1)
                        _objects.Add(new Wall(rect) { Shader = DESIGNER_WALL, MapIndex = (i, j) });
                    else if (_level.Map[i, j] == 2)
                        _objects.Add(new Door(rect) { Shader = DESIGNER_DOOR, MapIndex = (i, j) });
                }


            }

            for (int i = 0; i < _level.Enemies.Count; i++)
            {
                _objects.Add(new Enemy(new(_level.Enemies[i], new(20, 20))));
            }

        }

        private void ResizeMap(int width, int height)
        {
            if (width < 0 || height < 0) return;
            int[,] map = new int[width, height];

            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    if (_level.MapHeight <= i || _level.MapWidth <= j)
                    {
                        map[i, j] = 0;
                    }
                    else map[i, j] = _level.Map[i, j];
                }
            }

            _level = new(map, _level.Enemies)
            {
                MapHeight = height,
                MapWidth = width
            };

            CreateMap();
        }
    }
}
