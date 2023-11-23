using System.ComponentModel;

namespace FormsLib.Controls;

public partial class MenuOption : Control
{
    Label label;
    bool selected = false;
    public MenuOption()
    {
        InitializeComponent();
        label = new();
        label.ForeColor = Color.Yellow;
        Font impactFont = new Font("Impact", 18);
        label.Font = impactFont;
        Controls.Add(label);
        label.Size = new(100, 100);
    }

    [Category("Misc")]
    [Browsable(true)]
    [Description("An option in a menu")]
    public string OptionText
    {
        get
        {
            return label.Text;
        }
        set
        {
            label.Text = value;
        }
    }

    [Category("Misc")]
    [Browsable(true)]
    [Description("The unselected colour")]
    public Color DefaultColor { get; set; } = Color.Yellow;

    [Category("Misc")]
    [Browsable(true)]
    [Description("A toggle for if the option is selected by default")]
    public bool Selected
    {
        get
        {
            return selected;
        }
        set
        {
            selected = value;
            if (value) label.ForeColor = Color.White;
            else label.ForeColor = DefaultColor;
        }
    } 

    protected override void OnPaint(PaintEventArgs pe)
    {
        base.OnPaint(pe);
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }
}
