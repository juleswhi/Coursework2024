namespace UserDetails;

public class Name : IEquatable<Name>
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
    


    // Implement IEquatable

    public bool Equals(Name? other)
    {
        if (other is null) return false;

        if (other.Username == this.Username) return true;

        return false;
    }

    public static bool operator ==(Name? left, Name? right) => left!.Username == right!.Username;
    
    public static bool operator !=(Name? left, Name? right) =>  !(left!.Username == right!.Username);




}
