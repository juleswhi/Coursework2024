using AuthenticationLib.Login;
namespace UserDetails;
public class Admin : IUser
{
    public AuthLevel AuthLevel => AuthLevel.Admin;
    public AuthDetails AuthDetails { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Name? Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

}
