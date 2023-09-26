namespace AuthenticationLib.Login;


public static class LoginManager
{
    public static void Login(string username, string password) =>
       User.Users
       .Where(x => x.Name!.Username == username)
       .Where(x => x.AuthDetails.Password == password)
       .FirstOrDefault()?
       .SetCurrentUser();


    public static User? CurrentUser { get; private set; } = null;
    public static void Logout(this User _) => CurrentUser = null;
    private static void SetCurrentUser(this User user) => CurrentUser = user;

}
