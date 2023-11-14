using System.Diagnostics.Tracing;

namespace FormsLib.Platformer;

public partial class formPlatformer : Form
{
    public formPlatformer()
    {
        InitializeComponent();

        new Game(this, labelWin);
    }



    private void orangePlatform_Click(object sender, EventArgs e)
    {

    }

    private void Player_Click(object sender, EventArgs e)
    {

    }
}
