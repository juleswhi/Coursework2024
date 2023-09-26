using System.Data;
using Chess.Pieces;
using System.Windows.Forms;
using FormsLib.Chess;
using FormsLib.Menus;

namespace Helpers;

public static class ChessHelper
{
    public static List<Square> Squares = new();
    public static Notation GetNotation(this (int, int) coordinates) => coordinates switch
    {
        (1, int Row) => new Notation('a', Row),
        (2, int Row) => new Notation('b', Row),
        (3, int Row) => new Notation('c', Row),
        (4, int Row) => new Notation('d', Row),
        (5, int Row) => new Notation('e', Row),
        (6, int Row) => new Notation('f', Row),
        (7, int Row) => new Notation('g', Row),
        (8, int Row) => new Notation('h', Row),
        _ => throw new RowNotInTableException()
    };

    public static string ImageDirectory = "../../../../FormsLib/Chess/Images/";

    public static void ShowImage(this Piece piece, Func<Piece, Square, bool> IsRightSquare)
    {
        Square CorrectSquare = null;

        foreach(var i in Squares)
        {
            if(IsRightSquare(piece, i)) CorrectSquare = i;
        }

        if (CorrectSquare == null) return;

        Image img = Image.FromFile($"{ImageDirectory}{piece.Colour.ToString()}{piece.Type.ToString()}.png");

        CorrectSquare.AddControl(img);

        // CorrectSquare.AddText(piece.Type.ToString());

        


    }
}
