using UserDetails;
namespace AuthenticationLib.Login;


public static class Login
{
    public static bool LoginUser(string username, string password) {
        return true; 
    }
    public static User? CurrentUser { get; private set; } = null;


}
