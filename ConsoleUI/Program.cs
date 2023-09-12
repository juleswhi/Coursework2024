using UserDetails;
using Serialization;
using Filepaths;
using Hash;



List<IUser> users = new();

Name cosmin = new("Cosmin", "Ursache", 123);

users.Add(new User(new AuthDetails(cosmin.Username, "Password123".Hash()), cosmin) );

users.Serialize(FilepathManager.UserDetails);

