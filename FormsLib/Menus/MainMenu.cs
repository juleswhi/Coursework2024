using FormsLib.Chess;
namespace FormsLib.Menus
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            button1.MouseHover += ButtonHover;
            button1.MouseLeave += ButtonLeave;
        }

        private void ButtonLeave(object? sender, EventArgs e)
        {
            var button = sender as Button;
            button.BackColor = Color.Gray;
        }

        private void ButtonHover(object? sender, EventArgs e)
        {
            var button = sender as Button;
            button.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            (ActiveForm as MenuHolder)!.OpenChildForm(new formBoard());
        }

    }
}
