using Filepaths;
namespace AuthenticationLib.Misc;

public static class Extension
{
    public static Code AddUser(this User user)
    {
        List<User> users = new() { user };
        users.AddRange(User.Users);
        return users.Serialize(FilepathManager.UserDetails);
    }
}
