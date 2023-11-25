using RayEngine.Levels;

namespace RayEngine
{
    public partial class formRaycaster : Form
    {
        public formRaycaster()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            new Raycaster(this, "LevelOne");
        }

    }
}
