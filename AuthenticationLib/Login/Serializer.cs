using System.Runtime.Serialization.Json;
using Filepaths;

namespace Serialization;

public static class Serializer
{

    public static Code Serialize(this User user)
    {
        var list = User.Users;
        list.Add(user);
        return list.Serialize(FilepathManager.UserDetails);
    }
    public static Code Serialize<T>(this IEnumerable<T> list, string filepath)
    {
        try
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Create))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(IEnumerable<T>));
                serializer.WriteObject(fs, list);
            }
        }

        catch (FileNotFoundException) { return Code.FileNotFound; }
        return Code.Ok;
    }

    public static List<T> Deserialize<T>(this T _, string filepath)
    {
        try
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<T>));

                return (List<T>)serializer.ReadObject(fs)!;
            }
        }

        catch (FileNotFoundException) { }

        return new();
    }
}
