using System.Runtime.Serialization;

namespace UserDetails;

[DataContract]
public class Name
{
    public Name(string Forname, string Surname, string? Username = null)
    {
        this.Forname = Forname;
        this.Surname = Surname;
        if (Username != null) this.Username = Username;
    }
    public Name(string Forname, string Surname, int Number)
    {
        
        this.Forname = Forname;
        this.Surname = Surname;
        this.Username = $"{Forname[0]}{Surname}{Number}";
    }
    [DataMember]
    public string Username { get; set; }
    [DataMember]
    public string Forname { get; set; }
    [DataMember]
    public string Surname { get; set; }
}
