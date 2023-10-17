using Filepaths;
using AuthenticationLib.Login;
namespace FormsLib.Menus;

public partial class LoginMenu : Form
{
    public LoginMenu()
    {
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        // Try to login
        if (LoginManager.Login(txtBoxUsername.Text, txtBoxPassword.Text))
        {
            (ActiveForm as MenuHolder)!.OpenChildForm(new MainMenu());
            return;
        }

        // tell the user that their details are wrong
        lblUsernameError.Text = "Your username or passowrd is wrong. Please try again ";
        // Clear the txtBox for password
        txtBoxPassword.Text = "";
    }
}
