using AuthenticationLib.Login;

namespace UserDetails;

public interface IUser
{
    // Admin or User
    AuthLevel AuthLevel { get; }
    // Username and Password
    AuthDetails AuthDetails { get; set; }
    Name? Name { get; set; }
}
