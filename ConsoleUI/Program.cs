using UserDetails;
using Serialization;
using Filepaths;
using Hash;
using AuthenticationLib.Login;

AuthDetails authDetails = new("JWhite123", "password1".Hash());
Name name = new Name("Jules", "White", "JWhite123");

User jules = new(authDetails, name);

