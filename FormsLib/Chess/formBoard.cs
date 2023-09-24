using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helpers;
using FormsLib.Chess.Pieces;

namespace FormsLib.Chess;

public partial class formBoard : Form
{
    public List<Square> Squares { get; set; } = new();

    public List<Piece> WhitePieces { get; set; } = new();
    public List<Piece> BlackPieces { get; set; } = new();

    public FlowLayoutPanel Board { get; set; } = new();


    private static List<int> RookStartNumbers = new() { 1, 8 };
    private static List<int> BishopStartNumbers = new() { 3, 6 };
    private static List<int> HorseStartNumbers = new() { 2, 7 };
    private static List<int> RoyaltyStartNumbers = new() { 4, 5 };

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

        GetImages();
    }

    public void AddSquares()
    {
        // Create 64 squares
        Squares = Enumerable.Range(1, 8)
            .SelectMany(x => Enumerable.Range(1, 8), (x, y) =>
                new Square(x, y, Board))
            .ToList();

        // Print coords 
        // Squares.ForEach(x => x.AddText());
    }

    public void AddPieces()
    {
        // So much code oml

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


        WhitePieces.AddRange(Enumerable.Range(1, 2)
            .Select(x =>
            new Piece((PieceType)2, new Notation(HorseStartNumbers[x], 1))).ToList());

        BlackPieces.AddRange(Enumerable.Range(1, 8)
            .Select(x =>
            new Piece((PieceType)2, new Notation(HorseStartNumbers[x], 8))).ToList());


        // Two Bishops


        WhitePieces.AddRange(Enumerable.Range(1, 2)
            .Select(x =>
                new Piece((PieceType)3, new Notation(BishopStartNumbers[x], 1))).ToList());

        BlackPieces.AddRange(Enumerable.Range(1, 2)
            .Select(x =>
                new Piece((PieceType)3, new Notation(BishopStartNumbers[x], 8))).ToList());

        // Two Rooks


        WhitePieces.AddRange(Enumerable.Range(1, 2)
            .Select(x =>
                new Piece((PieceType)4, new Notation(RookStartNumbers[x], 1))).ToList());

        BlackPieces.AddRange(Enumerable.Range(1, 2)
            .Select(x =>
                new Piece((PieceType)4, new Notation(RookStartNumbers[x], 8))).ToList());

        // One King and One Queen


        WhitePieces.AddRange(Enumerable.Range(1, 2)
            .Select(x =>
                new Piece((PieceType)(x + 4), new Notation(RoyaltyStartNumbers[x], 1))).ToList());

        BlackPieces.AddRange(Enumerable.Range(1, 2)
            .Select(x =>
                new Piece((PieceType)(x + 4), new Notation(RoyaltyStartNumbers[x], 8))).ToList());


        // Give each piece a Colour

        WhitePieces.ForEach(x => x.Colour = PieceColour.White);
        BlackPieces.ForEach(x => x.Colour = PieceColour.Black);
    }


    private void GetImages()
    {
        for(int i = 0; i < WhitePieces.Count; i++)
        {
            Image img = GetPieceImages($"{(PieceColour)1}{(PieceType)i}");
            Label PieceImageLabel = new();
            PieceImageLabel.Image = img;
            Squares.Where(x =>
                IsSameNotation(x.Notation))
            .ToList()
            .ForEach((x) =>
            {
                // Pawns
                PieceImageLabel.Show();
                if (x.Notation.Rank == 2)
                {
                    x.Controls.Add(PieceImageLabel);
                }
                // else if(x.Notation.Notation == new Notation())
            });
        }

    }

    private bool IsSameNotation(Notation notation1)
    {
        foreach(var i in WhitePieces)
            if (i.Notation == notation1) return true;
        foreach(var i in BlackPieces)
            if (i.Notation == notation1) return true;
        return false;
    }

    private Image GetPieceImages(string ImageName) => Image.FromFile($"{ChessHelper.ImageDirectory}{ImageName}.png");
}
