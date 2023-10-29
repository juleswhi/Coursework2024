using UserDetails;
using Serialization;


namespace FormsLib.Menus
{
    public partial class formRegister : Form
    {
        public formRegister()
        {
            InitializeComponent();
        }

        public formRegister((string, string) details)
        {
            InitializeComponent();
            txtBoxUsername.Text = details.Item1;
            txtBoxPassword.Text = details.Item2;
        }

        private void btnBack_Click(object sender, EventArgs e) =>
            (ActiveForm as MenuHolder)!.OpenChildForm(new LoginMenu());

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // register
            var auth = new AuthDetails(txtBoxUsername.Text, txtBoxPassword.Text);
            var name = new Name(txtBoxForename.Text, txtBoxSurname.Text);

            User user = new(auth, name)
            {
                DOB = dtpDOB.Value,
                Email = txtBoxEmail.Text
            };
            
            // Create user

            user.Serialize();

            // Go back to main menu
            
            (ActiveForm as MenuHolder)!.OpenChildForm(new LoginMenu());

        }
    }
}
