using UserDetails;
namespace AuthenticationLib.Login;


public static class Login
{
    public static bool IsAuthenticated(this IUser user)
    {



        return true;
    }

    public static unsafe User* CurrentLoggedIn { get; set; }
}
