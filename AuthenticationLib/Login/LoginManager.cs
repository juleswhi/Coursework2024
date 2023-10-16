using Hash; 
namespace AuthenticationLib.Login;


public static class LoginManager
{
    public static bool Login(string username, string password)
    {
        User? user = User.Users
            .Where(x => x.AuthDetails.Username == username && x.AuthDetails.Password == password.Hash())
            .FirstOrDefault();

        if (user is null) return false;

        CurrentUser = user;

        return true;
    }


    public static User? CurrentUser { get; private set; } = null;
    public static void Logout(this User _) => CurrentUser = null;

}
