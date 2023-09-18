using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsLib.Menus
{
    public partial class MenuHolder : Form
    {
        private Form activeForm;
        public MenuHolder()
        {
            InitializeComponent();
            OpenChildForm(new LoginMenu());
        }


        public void OpenChildForm(Form childForm)
        {
            if(activeForm != null) activeForm.Close();

            activeForm = childForm;
            activeForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            this.panelHolder.Controls.Add(childForm);
            this.panelHolder.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
    }
}
