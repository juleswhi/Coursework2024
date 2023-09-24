using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsLib.Chess;

public partial class formBoard : Form
{
    public List<Square> Squares { get; set; } = new();
    public FlowLayoutPanel Board { get; set; } = new();
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
    }

    public void AddSquares()
    {
        Squares = Enumerable.Range(1, 8)
            .SelectMany(x => Enumerable.Range(1, 8), (x, y) =>
                new Square(x, y, Board))
            .ToList();

        Squares.ForEach(x => x.AddText());
    }
}
