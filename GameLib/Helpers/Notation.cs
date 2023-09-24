namespace Helpers;

public struct Notation
{

    public Notation(char File, int Row)
    {
        this.File = File;
        this.Row = Row;
    }

    public Notation(int File, int Row)
    {
        this.File = (File, Row).GetNotation().File;
        this.Row = Row;
    }

    public char File { get; set; }
    public int Row { get; set; }

    public (char, int) Square => (File, Row);


    public override string ToString() => $"{this.File}{this.Row}";
}
