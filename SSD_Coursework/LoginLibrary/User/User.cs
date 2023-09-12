using AuthenticationLib.Login;

namespace UserDetails;

public class User : IUser
{
    
    public AuthLevel AuthLevel { get; set; }
    public AuthDetails AuthDetails { get; set; }
    public Name Name { get; set; }
    public bool IsLoggedIn  => Login.CurrentUser == this.Name ? true : false;

}
