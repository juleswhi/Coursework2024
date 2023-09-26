namespace FormsLib.Menus
{
    public partial class MenuHolder : Form
    {
        public Form activeForm { get; set; }
        public MenuHolder()
        {
            InitializeComponent();
            OpenChildForm(new LoginMenu());
        }


        public void OpenChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();

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
