namespace User;

public struct Name
{
    public Name(string Forname, string Surname)
    {
        this.Forname = Forname;
        this.Surname = Surname;
    }
    public string Forname { get; set; }
    public string Surname { get; set; }
}
