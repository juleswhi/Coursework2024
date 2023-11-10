namespace FormsLib.Platformer;

public partial class formPlatformer : Form
{
    public formPlatformer()
    {
        InitializeComponent();

        new Game(this, Player);
    }


    private void orangePlatform_Click(object sender, EventArgs e)
    {

    }
}
