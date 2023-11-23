using AuthenticationLib.Login;
using FormsLib.Controls;
using System.Security.Principal;

namespace FormsLib.Menus;

public partial class LoginMenu : Form
{
    

    List<MenuOption> options = new();
    int index = 0;
    public LoginMenu()
    {
        InitializeComponent();
        foreach (var control in Controls)
            if (control is MenuOption)
                options.Add((control as MenuOption)!);

        options[2].Selected = true;
    }

    private void LoginMenu_KeyDown(object? sender, KeyEventArgs e)
    {

        switch(e.KeyValue)
        {
            /*
            case Keys.Up:
                if (index == options.Count) index = 0;
                foreach (var op in options) op.Selected = false;
                index++;
                break;
            */
            case 27:
            case 91:
            case 65:
                if (index < 0) index = options.Count - 1;
                foreach (var op in options) op.Selected = false;
                options[index].Selected = true;
                index--;
                break;
        }
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        /* Try to login
        if (LoginManager.Login(txtBoxUsername.Text, txtBoxPassword.Text))
        {
            (ActiveForm as MenuHolder)!.OpenChildForm(new MainMenu());
            return;
        }

        // tell the user that their details are wrong
        lblUsernameError.Text = "Your username or passowrd is wrong. Please try again ";
        // Clear the txtBox for password
        // txtBoxPassword.Text = "";
        */
    }

    private void btnRegister_Click(object sender, EventArgs e) =>
        (ActiveForm as MenuHolder)!.OpenChildForm
        (new formRegister(("", "")));
}
