using AuthenticationLib.Login;
using AuthenticationLib.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserDetails;

namespace FormsLib.Menus
{
    public partial class formRegister : Form
    {
        public formRegister()
        {
            InitializeComponent();
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



             
        }
    }
}
