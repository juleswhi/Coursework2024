using Newtonsoft.Json;
using System.Runtime.Serialization.Json;

namespace RayEngine.Levels;


[Serializable]
public class LevelData
{
    public int[,] Map { get; set; }
    public int MapWidth { get; set; }
    public int MapHeight { get; set; }
    public List<Point> Enemies { get; set; }
    public LevelData(int[,] Map, List<Point> Enemies)
    {
        this.Map = Map;
        this.Enemies = Enemies;
    }
}

public static class MapSerializer
{
    public static void Serialize(this LevelData data, string filepath)
    {
        try
        {
            using (StreamWriter sw = new(filepath, false))
                sw.Write(JsonConvert.SerializeObject(data));
        }

        catch (FileNotFoundException) { return; }
    }

    public static LevelData? Deserialize(string filepath)
    {
        try
        {
            string data = File.ReadAllLines(filepath)[0];
            LevelData lvl = JsonConvert.DeserializeObject<LevelData>(data)!;
            return lvl;
        }
        catch (FileNotFoundException) { return null; }
    }

}
