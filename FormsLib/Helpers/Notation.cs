namespace Helpers;

public struct Notation : IEquatable<Notation>
{
    public Notation(char File, int Row)
    {
        this.File = File;
        this.Rank = Row;
    }

    public Notation(int File, int Row)
    {
        this.File = (File, Row).GetNotation().File;
        this.Rank = Row;
    }

    public char File { get; set; }
    public int Rank { get; set; }

    public (char, int) Number => (File, Rank);

    public bool Equals(Notation other) => this.ToString() == other.ToString();
    public static bool operator ==(Notation one, Notation two) => one.Equals(two);
    public static bool operator !=(Notation one, Notation two) => !one.Equals(two);

    public override string ToString() => $"{this.File}{this.Rank}";

    public override bool Equals(object obj)
    {
        return obj is Notation && Equals((Notation)obj);
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}
