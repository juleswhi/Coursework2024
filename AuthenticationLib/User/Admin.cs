using AuthenticationLib.Login;
namespace UserDetails;
public class Admin : User
{
    public override AuthLevel AuthLevel => AuthLevel.Admin;
}
