using System.IO;
using UnityEngine;

public static class FileManager
{
    public static readonly string PathToSave = Application.dataPath + "/save.json";

    public static void Save(string content)
    {
        File.WriteAllText(PathToSave, content);
    }

    public static string Load()
    {
        if (File.Exists(PathToSave))
        {
            return File.ReadAllText(PathToSave);
        }
        else
        {
            return null;
        }
    }
}
