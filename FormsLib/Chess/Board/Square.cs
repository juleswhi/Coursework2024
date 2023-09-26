using Helpers;

namespace FormsLib.Chess;

public class Square : Panel
{

    public Label Label = new Label();
    public Square(int i, int j, FlowLayoutPanel flp)
    {
        SquareNumber = i + j;
        Notation = new Notation(i, j);
        Parent = flp;
        SetupSquare();
    }


    public Square(int SquareNumber, FlowLayoutPanel flp)
    {
        this.SquareNumber = SquareNumber;
        Parent = flp;
        SetupSquare();
    }


    public void SetupSquare()
    {
        BackColor = SquareNumber % 2 == 0 ? BlackColour : WhiteColour;

        Size = new Size(SIZE, SIZE);
        var pad = new Padding(0, 0, 0, 0);
        Padding = pad;
        Margin = pad;


        this.Controls.Add(Label);
    }

    public const int SIZE = 50;
    public int SquareNumber { get; set; }
    public Notation Notation { get; set; }


    // Predefined colours for squares
    private Color WhiteColour = Color.FromArgb(206, 222, 189);
    private Color BlackColour = Color.FromArgb(67, 83, 52);

    // Label the squares
    public void AddText()
    {
        Label label = new Label();
        label.Text = Notation.ToString();
        Controls.Add(label);
    }

    public override string ToString() => SquareNumber.ToString();

}
