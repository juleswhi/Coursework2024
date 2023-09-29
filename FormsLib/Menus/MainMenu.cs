using FormsLib.Chess;
namespace FormsLib.Menus
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            (ActiveForm as MenuHolder)!.OpenChildForm(new formBoard());
        }
    }
}
