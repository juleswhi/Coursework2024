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

            if (CurrentlySelectedPiece.Colour != formBoard.ColourToMove)
            {
                CurrentlySelectedPiece = null;
                return;
            }

            var pieceMoves = CurrentlySelectedPiece.GetMoves(); 

            foreach(var i in pieceMoves)
            {
                var square = i.SquareFromNotation();
                square.BackColor = Color.Orange;
            }

            SquareSelectedNormalColor = !SquareSelectedNormalColor;
            BackColor = SquareSelectedNormalColor ? (SquareNumber % 2 == 0 ? BlackColour : WhiteColour) : Color.Blue;

            return;
        }

        Square MoveFromSquare = CurrentlySelectedPiece.SquareFromPiece();

        var moves = CurrentlySelectedPiece.GetMoves();
        foreach (var i in moves)
        {
            var square = i.SquareFromNotation();
            square.BackColor = Color.Orange;
        }




        // if clicked on piece
        if (sender is Label)
        {
            
            Piece SelectedPiece = this.PieceFromSquare();
            // If clicked on self, remove click effect
            if(CurrentlySelectedPiece.Notation == this.Notation)
            {
                CurrentlySelectedPiece = null;
                RemoveColours();
                return;
            }

            // If move isnt a move return
            if (!moves.Contains(this.Notation)) return;


            // Take sender
            SelectedPiece.TakePiece();
            // Move the actual piece
            CurrentlySelectedPiece!.Move(this.Notation);
            RemoveColours();
            // CurrentlySelectedPiece = null;
            formBoard.ColourToMove = formBoard.ColourToMove == PieceColour.White ? PieceColour.Black : PieceColour.White;
            // Remove all the leftover sprites
            MoveFromSquare.Controls.Clear();
            // Draw new sprites
            formBoard.Draw();
            // Highlight new piece move
            this.BackColor = Color.Green;

            CurrentlySelectedPiece = null;

        }
        else
        {
            if (!moves.Contains(this.Notation)) return;
            // Remove artifacts
            MoveFromSquare.Controls.Clear();
            // Move the piece
            CurrentlySelectedPiece!.Move(this.Notation);

            RemoveColours();
            this.BackColor = Color.Green;
            CurrentlySelectedPiece = null;
            formBoard.ColourToMove = formBoard.ColourToMove == PieceColour.White ? PieceColour.Black : PieceColour.White;
        }
    }

    private static void RemoveColours() => ChessHelper.Squares.ForEach(x =>
    {
        x.BackColor = x.SquareNumber % 2 == 0 ? x.BlackColour : x.WhiteColour;
    });

    public override string ToString() => SquareNumber.ToString();

}
