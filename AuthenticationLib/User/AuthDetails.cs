using Hash;
namespace UserDetails;

public struct AuthDetails
{
    public AuthDetails(string Username, string Password)
    {
        this.Username = Username;
        this.Password = PasswordHasher.Hash(Password);
    }
    public string Username { get; set; }
    public string Password { get; set; }

}
