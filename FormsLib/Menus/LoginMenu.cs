using Filepaths;
using Serialization;
using AuthenticationLib.Login;
using UserDetails;

namespace FormsLib.Menus;

public partial class LoginMenu : Form
{
    public LoginMenu()
    {
        InitializeComponent();
        var user = new User(new AuthDetails("admin", "Password"));
        List<User> users = new() { user };
        Serializer.Serialize(users, FilepathManager.UserDetails);
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        // Login here

        

        string username = txtBoxUsername.Text;
        string password = txtBoxPassword.Text;

        LoginManager.Login(username, password);
        if (username == null || password == null) return;

        if (LoginManager.CurrentUser is null) return;

        (ActiveForm as MenuHolder)!.OpenChildForm(new MainMenu());
    }
}
