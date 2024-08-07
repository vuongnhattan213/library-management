using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class JsonFileManager
{
    private static JsonFileManager _instance;
    private static readonly object _lock = new object();

    private JsonFileManager() { }

    public static JsonFileManager Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new JsonFileManager();
                }
                return _instance;
            }
        }
    }

    public List<T> LoadFromFile<T>(string filePath)
    {
        if (!File.Exists(filePath)) return new List<T>();
        var jsonData = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<T>>(jsonData);
    }

    public void SaveToFile<T>(string filePath, List<T> data)
    {
        var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }
}