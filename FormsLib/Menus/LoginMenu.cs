namespace FormsLib.Menus;

public partial class LoginMenu : Form
{
    public LoginMenu()
    {
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        // Login here

        string username = txtBoxUsername.Text;
        string password = txtBoxPassword.Text;

        if (username == null || password == null) return;

        (ActiveForm as MenuHolder)!.OpenChildForm(new MainMenu());
    }
}
