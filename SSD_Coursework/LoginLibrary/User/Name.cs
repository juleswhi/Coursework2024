namespace UserDetails;

public struct Name
{
    public Name(string Forname, string Surname)
    {
        this.Forname = Forname;
        this.Surname = Surname;
    }
    public Name(string Forname, string Surname, int Number)
    {
        
        this.Forname = Forname;
        this.Surname = Surname;
        this.Username = $"{Forname[0]}{Surname}{Number}";
    }
    public string Username { get; set; }
    public string Forname { get; set; }
    public string Surname { get; set; }
}
