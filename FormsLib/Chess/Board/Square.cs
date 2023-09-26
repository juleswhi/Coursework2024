using Chess.Pieces;
using Helpers;

namespace FormsLib.Chess;

public class Square : Panel
{
    public const int SIZE = 50;
    public int SquareNumber { get; set; }
    public Notation Notation { get; set; }
    private bool SquareSelectedNormalColor { get; set; } = true;

    public static Piece CurrentlySelectedPiece { get; set; } = null;



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
        this.Click += ChangeSquareColour!;
    }



    // Predefined colours for squares
    private Color WhiteColour = Color.FromArgb(206, 222, 189);
    private Color BlackColour = Color.FromArgb(67, 83, 52);

    // Label the squares
    public void AddControl(string txt)
    {
        Label label = new Label();
        label.Text = txt;
        Controls.Add(label);
    }

    public void AddControl(Image img)
    {
        Label label = new Label();
        // Center image properly
        label.Dock = DockStyle.Fill;
        label.Image = img;
        label.Click += ChangeSquareColour!;
        Controls.Add(label);
    }

    private void ChangeSquareColour(object sender, EventArgs e)
    {

        if (CurrentlySelectedPiece is null)
        {
            if (sender is not Label) return;
            CurrentlySelectedPiece = this.PieceFromSquare()!;
            SquareSelectedNormalColor = !SquareSelectedNormalColor;
            BackColor = SquareSelectedNormalColor ? (SquareNumber % 2 == 0 ? BlackColour : WhiteColour) : Color.Blue;
        }


        if (sender is Label) { }
        else
        {
            Square MoveFromSquare = CurrentlySelectedPiece.SquareFromPiece();


            MoveFromSquare.Controls.Clear(); 
            CurrentlySelectedPiece!.Move(this.Notation);

            RemoveColours();
            CurrentlySelectedPiece = null;
        }
    }

    private static void RemoveColours() => ChessHelper.Squares.ForEach(x =>
    {
        x.BackColor = x.SquareNumber % 2 == 0 ? x.BlackColour : x.WhiteColour;
    });

    public override string ToString() => SquareNumber.ToString();

}
