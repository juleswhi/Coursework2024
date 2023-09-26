using Helpers;
namespace Chess.Pieces;

public class Piece
{
    public Piece(PieceType Type, PieceColour? Colour = null)
    {
        this.Type = Type;
        if (Colour is null) return;
        this.Colour = (PieceColour)Colour;

    }

    public Piece(PieceType Type, Notation Notation, PieceColour? Colour = null)
    {
        this.Type = Type;
        this.Notation = Notation;
        if (Colour == null) return;
        this.Colour = (PieceColour)Colour;
    }

    private void GetCorrectImage() => Image = Image.FromFile($"{ChessHelper.ImageDirectory}{Colour}{Type}.png");


    public PieceType Type { get; set; }
    public PieceColour Colour { get; set; }
    public Notation Notation { get; set; }
    public Image Image { get; set; }


}
