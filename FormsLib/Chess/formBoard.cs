using System.Data;
using Chess.Pieces;
using Helpers;

namespace FormsLib.Chess;

public partial class formBoard : Form
{
    public static List<Square> Squares { get; set; } = new();

    public static List<Piece> WhitePieces { get; set; } = new();
    public static List<Piece> BlackPieces { get; set; } = new();

    public FlowLayoutPanel Board { get; set; } = new();


    private static List<int> RookStartNumbers = new() { 1, 8 };
    private static List<int> BishopStartNumbers = new() { 3, 6 };
    private static List<int> HorseStartNumbers = new() { 2, 7 };
    private static List<int> RoyaltyStartNumbers = new() { 4, 5 };

    public static void Draw()
    {
        foreach (var piece in WhitePieces.Concat(BlackPieces))
            piece.ShowImage((piece, square) => piece.Notation == square.Notation);
    }

    public formBoard()
    {
        InitializeComponent();

        // The parent FLP should go left -> right
        this.flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
        this.flowLayoutPanel1.Dock = DockStyle.Fill;

        // Child flp should go down -> up
        Board.FlowDirection = FlowDirection.BottomUp;
        Board.Size = new Size(400, 400);

        // Create the squares
        AddSquares();

        flowLayoutPanel1.Controls.Add(Board);

        AddPieces();

        Draw();


    }

    public void AddSquares() =>
        Squares = Enumerable.Range(1, 8)
            .SelectMany(x => Enumerable.Range(1, 8), (x, y) =>
                new Square(x, y, Board))
            .ToList();

    public void AddPieces()
    {
        // 8 Pawns
        WhitePieces.AddRange(Enumerable.Range(1, 8)
            .Select(x =>
                new Piece((PieceType)1, new Notation(x, 2)))
            .ToList());

        BlackPieces.AddRange(Enumerable.Range(1, 8)
            .Select(x =>
                new Piece((PieceType)1, new Notation(x, 7)))
            .ToList());

        // Two Knights


        WhitePieces.AddRange(Enumerable.Range(0, 2)
            .Select(x =>
            new Piece((PieceType)2, new Notation(HorseStartNumbers[x], 1))).ToList());

        BlackPieces.AddRange(Enumerable.Range(0, 2)
            .Select(x =>
            new Piece((PieceType)2, new Notation(HorseStartNumbers[x], 8))).ToList());


        // Two Bishops


        WhitePieces.AddRange(Enumerable.Range(0, 2)
            .Select(x =>
                new Piece((PieceType)3, new Notation(BishopStartNumbers[x], 1))).ToList());

        BlackPieces.AddRange(Enumerable.Range(0, 2)
            .Select(x =>
                new Piece((PieceType)3, new Notation(BishopStartNumbers[x], 8))).ToList());

        // Two Rooks


        WhitePieces.AddRange(Enumerable.Range(0, 2)
            .Select(x =>
                new Piece((PieceType)4, new Notation(RookStartNumbers[x], 1))).ToList());

        BlackPieces.AddRange(Enumerable.Range(0, 2)
            .Select(x =>
                new Piece((PieceType)4, new Notation(RookStartNumbers[x], 8))).ToList());

        // One King and One Queen


        WhitePieces.AddRange(Enumerable.Range(0, 2)
            .Select(x =>
                new Piece((PieceType)(x + 5), new Notation(RoyaltyStartNumbers[x], 1))).ToList());

        BlackPieces.AddRange(Enumerable.Range(0, 2)
            .Select(x =>
                new Piece((PieceType)(x + 5), new Notation(RoyaltyStartNumbers[x], 8))).ToList());


        // Give each piece a Colour

        WhitePieces.ForEach(x => x.Colour = PieceColour.White);
        BlackPieces.ForEach(x => x.Colour = PieceColour.Black);
    }

}
