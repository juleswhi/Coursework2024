using AuthenticationLib.Login;

namespace UserDetails;

[Serializable]
public class User : IUser, IEquatable<User>
{
    public User(AuthDetails AuthDetails, Name? Name = null)
    {
        this.Name = Name; 
        this.AuthDetails = AuthDetails;
    }

    public AuthLevel AuthLevel => nameof(this.AuthLevel) == nameof(AuthLevel.User) ? AuthLevel.User : AuthLevel.Admin;
    public AuthDetails AuthDetails { get; set; }
    public Name? Name { get; set; }
    public bool IsLoggedIn  => Login.CurrentUser == this ? true : false;


    // IEquatable<> Overrides
    public bool Equals(User? other) => this == other!;
    public override bool Equals(object other) => (this == (User)other)!;
    public override int GetHashCode() => base.GetHashCode();
    public static bool operator ==(User a, User b) => a.Name!.Username == b.Name!.Username;
    public static bool operator !=(User a, User b) => !(a.Name!.Username == b.Name!.Username);

    // ToString override
    public override string ToString() => 
        $"{this.Name!.Forname},{this.Name!.Surname},{this.Name!.Username},{this.AuthLevel.ToString()},{this.AuthDetails.Password}";

}
