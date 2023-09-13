using AuthenticationLib.Login;

namespace UserDetails;

public interface IUser
{
    AuthLevel AuthLevel { get; }
    AuthDetails AuthDetails { get; set; }
    Name? Name { get; set; }
}
