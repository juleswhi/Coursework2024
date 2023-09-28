using System.Data;
using Chess.Pieces;
using FormsLib.Chess;

namespace Helpers;

public static class ChessHelper
{
    public static List<Square> Squares => formBoard.Squares;
    public static List<Piece> Pieces => formBoard.WhitePieces.Concat(formBoard.BlackPieces).ToList();
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

        foreach (var i in Squares)
        {
            if (IsRightSquare(piece, i)) CorrectSquare = i;
        }

        if (CorrectSquare == null) return;

        Image img = Image.FromFile($"{ImageDirectory}{piece.Colour.ToString()}{piece.Type.ToString()}.png");

        CorrectSquare.AddControl(img);
    }
    public static void ShowImage(this Piece piece)
    {
        Square CorrectSquare = null;

        foreach (var i in Squares)
        {
            if (i.Notation == piece.Notation) CorrectSquare = i;
        }

        if (CorrectSquare == null) return;

        Image img = Image.FromFile($"{ImageDirectory}{piece.Colour.ToString()}{piece.Type.ToString()}.png");

        CorrectSquare.AddControl(img);
    }


    public static void Move(this Piece piece, Notation notation)
    {
        piece.Notation = notation;
        formBoard.Draw();
    }

    public static Square SquareFromPiece(this Piece piece, Func<Piece, Square, bool> IsRightSquare)
    {
        foreach (var square in Squares)
            if (IsRightSquare(piece, square)) return square;
        return null;
    }

    public static Square SquareFromPiece(this Piece piece)
    {
        foreach (var square in Squares)
            if (square.Notation == piece.Notation) return square;
        return null;
    }

    public static Piece? PieceFromSquare(this Square square, Func<Piece, Square, bool> IsRightPiece)
    {
        foreach (var piece in Pieces)
            if (IsRightPiece(piece, square)) return piece;
        return null;
    }

    public static Piece? PieceFromSquare(this Square square)
    {
        foreach (var piece in Pieces)
            if (square.Notation == piece.Notation) return piece;
        return null;
    }

    public static void TakePiece(this Piece piece)
    {
        // Remove from list
        // remove sprite

        if (piece.Colour == PieceColour.White)
        {
            foreach (var peeece in formBoard.WhitePieces.Where(x => x.Notation == piece.Notation))
            {
                formBoard.WhitePieces.Remove(peeece);
                break;
            }
        }
        else foreach (var peeece in formBoard.BlackPieces.Where(x => x.Notation == piece.Notation))
        {
            formBoard.BlackPieces.Remove(peeece);
                break;
        }

        Square square = piece.SquareFromPiece();

        square.Controls.Clear();



        formBoard.Draw();
    }

    public static List<Notation> GetMoves(this Piece piece)
    {
        var moves = new List<Notation>();

        foreach(var square in formBoard.Squares)
        {
            moves.Add(square.Notation);
            moves.Add(new Notation(square.Notation.Rank, square.Notation.File + 1));
        }

        return moves;
    }

    public static Square SquareFromNotation(this Notation notation)
    {
        foreach(var square in formBoard.Squares)
        {
            if (square.Notation == notation) return square;
        }
        return null;
    }
}
