using AuthenticationLib.Login;
using Filepaths;

namespace UserDetails;

[Serializable]
public class User : IUser, IEquatable<User>
{

    public static List<User> Users
    {
        get
        {
            return new User().Deserialize(FilepathManager.UserDetails);
        }
        set
        {
            Users = value;
        }
    }

    public DateTime DOB { get; set; }
    public string Email { get; set; }



    public User(AuthDetails AuthDetails, Name? Name = null)
    {
        this.Name = Name;
        this.AuthDetails = AuthDetails;
    }

    public User() { }

    public virtual AuthLevel AuthLevel => AuthLevel.User;
    public AuthDetails AuthDetails { get; set; }
    public Name? Name { get; set; }



    // This needs a comment lmao
    // IsLoggedIn is just asking if Current Logged in user has the same username as this one
    // If Login.CurrentUser is null, create a new user ( which will return false )
    // If it is the same, return true, else false

    public bool IsLoggedIn => (LoginManager.CurrentUser ?? new User()) == this ? true : false;


    // IEquatable<> Overrides
    public bool Equals(User? other) => this == other!;
    public override bool Equals(object other) => (this == (User)other)!;
    public override int GetHashCode() => base.GetHashCode();
    public static bool operator ==(User a, User b) => a.Name?.Username == b.Name?.Username;
    public static bool operator !=(User a, User b) => a.Name?.Username != b.Name?.Username;

    // ToString override
    public override string ToString() =>
        $"{this.Name!.Forname},{this.Name!.Surname},{this.Name!.Username},{this.AuthLevel.ToString()},{this.AuthDetails.Password}";

}

