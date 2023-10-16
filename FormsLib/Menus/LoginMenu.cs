using Filepaths;
using Serialization;
using AuthenticationLib.Login;
using UserDetails;
using Hash;

namespace FormsLib.Menus;

public partial class LoginMenu : Form
{
    public LoginMenu()
    {
        InitializeComponent();
        //var user = new User(new AuthDetails("admin", "Password"));
        //List<User> users = new() { user };
        //Serializer.Serialize(users, FilepathManager.UserDetails);
        //users.Serialize(FilepathManager.UserDetails);
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {

        string username = txtBoxUsername.Text;
        string password = txtBoxPassword.Text;

        if(LoginManager.Login(username, password))
        {
            lblLogin.Text = "Logged in";
            (ActiveForm as MenuHolder)!.OpenChildForm(new MainMenu());
            return;
        }

        // Did  not log in

    }
}
