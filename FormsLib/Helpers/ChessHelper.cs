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
        (1, int Row) => new Notation('a', Row).Floor(),
        (2, int Row) => new Notation('b', Row).Floor(),
        (3, int Row) => new Notation('c', Row).Floor(),
        (4, int Row) => new Notation('d', Row).Floor(),
        (5, int Row) => new Notation('e', Row).Floor(),
        (6, int Row) => new Notation('f', Row).Floor(),
        (7, int Row) => new Notation('g', Row).Floor(),
        (8, int Row) => new Notation('h', Row).Floor(),
        _ => new Notation('h', coordinates.Item2.Floor())
    };

    public static (int, int) GetNumbers(this Notation notation) => notation.File switch
    {
         'a' => (1, notation.Rank),
         'b' => (2, notation.Rank),
         'c' => (3, notation.Rank),
         'd' => (4, notation.Rank),
         'e' => (5, notation.Rank),
         'f' => (6, notation.Rank),
         'g' => (7, notation.Rank),
         'h' => (8, notation.Rank),
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

        var enemyPieces = piece.Colour == PieceColour.White ? formBoard.BlackPieces : formBoard.WhitePieces;

        if (piece.Type == PieceType.Pawn)
            moves.AddRange(piece.GetPawnAttacks(enemyPieces));

        else if (piece.Type == PieceType.Bishop)
            moves.AddRange(piece.GetBishopAttacks(enemyPieces));

        else if (piece.Type == PieceType.Rook)
            moves.AddRange(piece.GetRookAttacks(enemyPieces));

        else if (piece.Type == PieceType.Queen)
            moves.AddRange(piece.GetQueenAttacks(enemyPieces));

        else if (piece.Type == PieceType.Knight)
            moves.AddRange(piece.GetKnightAttacks(enemyPieces));

        else if (piece.Type == PieceType.King)
            moves.AddRange(piece.GetKingAttacks(enemyPieces));
        
        Square square = piece.SquareFromPiece();

        moves.ForEach(x => x.Floor());

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

    public static Notation AddFile(this Notation notation, int num) =>
        new Notation(notation.File + num, notation.Rank);
    public static Notation AddRank(this Notation notation, int num) =>
        new Notation(notation.File, notation.Rank + num);

    public static Notation Floor(this Notation notation)
    {
        if (notation.Rank > 8) notation.Rank = 8;
        else if (notation.Rank < 1) notation.Rank = 0;

        (int file, int _) = notation.GetNumbers();

        if (file > 8) notation.File = 'h';
        else if (file < 1) notation.File = 'a';

        return notation;
    }


    public static int Floor(this int num)
    {
        if (num > 8) num = 8;
        else if (num < 1) num = 1;
        return num;
    }

    public static List<Notation> GetPawnAttacks(this Piece piece, List<Piece> enemyPieces)
    {
        var moves = new List<Notation>();

        (int file, int rank) = piece.Notation.GetNumbers();

        int forwardDirection = (piece.Colour == PieceColour.White) ? 1 : -1;

        int[] deltaRanks = { forwardDirection, forwardDirection * 2 };
        int[] deltaFiles = { 0, -1, 1 };

        // Foward or doubleMove
        foreach(var deltaRank in deltaRanks)
        {
            int newRank = rank + deltaRank;
            if(newRank >= 1 && newRank <= 8)
            {
                var newPosition = new Notation(file, newRank);
                var pieceAtPosition = formBoard.WhitePieces.Concat(formBoard.BlackPieces).FirstOrDefault(piece => piece.Notation == newPosition);

                if(pieceAtPosition == null)
                {
                    moves.Add(newPosition);
                    if(deltaRank == forwardDirection && (piece.Colour == PieceColour.White && rank == 2 || piece.Colour == PieceColour.Black && rank == 7))
                    {
                        int twoMoverank = rank + forwardDirection * 2;
                        var towMoveLocation = new Notation(file, twoMoverank);

                        var piceAtTwoMove = formBoard.WhitePieces.Concat(formBoard.BlackPieces).FirstOrDefault(piece => piece.Notation == towMoveLocation);

                        if (piceAtTwoMove == null) moves.Add(towMoveLocation);
                    }
                }
            }
        }

        // Diag cap
        foreach(int deltaFile in deltaFiles)
        {
            int newFile = file + deltaFile;
            int newRank = rank + forwardDirection;

            if(newFile >= 1 && newFile <= 8 && newRank >= 1 && newRank <= 8)
            {
                var newPosition = new Notation(newFile, newRank);

                var pieceAtPosition = formBoard.WhitePieces.Concat(formBoard.BlackPieces).FirstOrDefault(piece => piece.Notation == newPosition);
                if(pieceAtPosition != null && pieceAtPosition.Colour != piece.Colour) moves.Add(newPosition);
            }
        }

        return moves;
    }
    public static List<Notation> GetBishopAttacks(this Piece piece, List<Piece> enemyPieces)
    {
        var moves = new List<Notation>();

        (int file, int rank) = piece.Notation.GetNumbers();

        int[] deltaFiles = { -1, 1, -1, 1 };
        int[] deltaRanks = { -1, -1, 1, 1 };

        for(int i = 0; i < 4; i++)
        {
            for(int distance = 1; distance <= 7; distance ++)
            {
                int newFile = file + deltaFiles[i] * distance;
                int newRank = rank + deltaRanks[i] * distance;

                if (newFile < 1 || newFile > 8 || newRank < 1 || newRank > 8)
                    break;

                var newPosition = new Notation(newFile, newRank);


                var pieceAtPosition = formBoard.WhitePieces.Concat(formBoard.BlackPieces).FirstOrDefault(piece => piece.Notation == newPosition);
               

                if (pieceAtPosition == null) moves.Add(newPosition);

                else
                {
                    if (pieceAtPosition.Colour != piece.Colour) moves.Add(newPosition);

                    break;
                }

            }
        }

        return moves;


    }
    public static List<Notation> GetRookAttacks(this Piece piece, List<Piece> enemyPieces)
    {
        var moves = new List<Notation>();
        (int file, int rank) = piece.Notation.GetNumbers();

        int[] deltaFiles = { 0, 0, -1, 1 };
        int[] deltaRanks = { -1, 1, 0, 0 };

        for(int i = 0; i < 4; i++)
        {
            for(int distance = 1; distance <= 7; distance ++)
            {
                int newFile = file + deltaFiles[i] * distance;
                int newRank = rank + deltaRanks[i] * distance;

                if (newFile < 1 || newFile > 8 || newRank < 1 || newRank > 8) break;

                var newPosition = new Notation(newFile, newRank);

                var pieceAtPosition = formBoard.WhitePieces.Concat(formBoard.BlackPieces).FirstOrDefault(piece => piece.Notation == newPosition);

                if (pieceAtPosition == null) moves.Add(newPosition);

                else
                {
                    if (pieceAtPosition.Colour != piece.Colour) moves.Add(newPosition);
                    break;
                }

            }
        }
        return moves;
    }
    public static List<Notation> GetQueenAttacks(this Piece piece, List<Piece> enemyPieces)
    {
        var moves = new List<Notation>();

        (int file, int rank) = piece.Notation.GetNumbers();

        int[] deltaFiles = { 0, 0, -1, 1, -1, 1, -1, 1 };
        int[] deltaRanks = { -1, 1, 0, 0, -1, -1, 1, 1 };

        for (int i = 0; i < 8; i++)
        {
            for (int distance = 1; distance <= 7; distance++)
            {
                int newFile = file + deltaFiles[i] * distance;
                int newRank = rank + deltaRanks[i] * distance;

                if (newFile < 1 || newFile > 8 || newRank < 1 || newRank > 8) break;

                var newPosition = new Notation(newFile, newRank);

                var pieceAtPosition = formBoard.WhitePieces.Concat(formBoard.BlackPieces).FirstOrDefault(piece => piece.Notation == newPosition);

                if (pieceAtPosition == null) moves.Add(newPosition);

                else
                {
                    if (pieceAtPosition.Colour != piece.Colour) moves.Add(newPosition);
                    break;
                }
            }
        }
        return moves;
    }    
    public static List<Notation> GetKnightAttacks(this Piece piece, List<Piece> enemyPieces)
    {
        var moves = new List<Notation>();

        (int file, int rank) = piece.Notation.GetNumbers();

        int[] deltaFiles = { 2, 1, -1, -2, -2, -1, 1, 2 };
        int[] deltaRanks = { 1, 2, 2, 1, -1, -2, -2, -1 };

        for(int i = 0; i < 8; i++)
        {
            int newFile = file + deltaFiles[i];
            int newRank = file + deltaRanks[i];

            if(newFile >= 1 && newFile <= 8 && newRank >= 1 && newRank <= 8)
            {
                var newPosition = new Notation(newFile, newRank);

                var pieceAtPosition = formBoard.WhitePieces.Concat(formBoard.BlackPieces).FirstOrDefault(piece => piece.Notation == newPosition);

                if (pieceAtPosition == null || pieceAtPosition.Colour != piece.Colour) moves.Add(newPosition);
            }
        }
        return moves;
    }
    public static List<Notation> GetKingAttacks(this Piece piece, List<Piece> enemyPieces)
    {
        var moves = new List<Notation>();
        (int file, int rank) = piece.Notation.GetNumbers();

        int[] deltaFiles = { -1, 0, 1, -1, 1, -1, 0, 1 };
        int[] deltaRanks = {-1, -1, -1, 0, 0, 1, 1, 1};

        for(int i = 0; i < 8; i ++)
        {
            int newFile = file + deltaFiles[i];
            int newRank = file + deltaRanks[i];

            if(newFile >= 1 && newFile <= 8 && newRank >= 1 && newRank <= 8)
            {
                var newPosition = new Notation(newFile, newRank);
                var pieceAtPosition = formBoard.WhitePieces.Concat(formBoard.BlackPieces).FirstOrDefault(piece => piece.Notation == newPosition);

                if (pieceAtPosition == null || pieceAtPosition.Colour != piece.Colour) moves.Add(newPosition);
            }
        }
        return moves;
    }
}
