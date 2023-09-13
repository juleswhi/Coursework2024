namespace Chess.Board;

public class Coordinate
{
    public int File { get; }
    public int Rank { get; }

    public Coordinate(int file, int rank)
    {
        this.File = file;
        this.Rank = rank;
    }



}
