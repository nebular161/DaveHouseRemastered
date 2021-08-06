using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    public static string directory = PlayerPrefs.GetString("PlayerName");
    public static string filename = "settings.davedata";

    public static void SaveSettings(Settings settings)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(GetFullPath());
        bf.Serialize(fs, settings);
        fs.Close();
    }
    public static Settings LoadSettings()
    {
        if(SaveExists())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(GetFullPath(), FileMode.Open);
                Settings settings = (Settings)bf.Deserialize(fs);
                fs.Close();

                return settings;
            }
            catch(SerializationException ex)
            {
                Debug.LogError("File loading failed: " + ex);
            }
        }

        return null;
    }
    private static bool SaveExists()
    {
        return File.Exists(GetFullPath());
    }
    private static bool DirectoryExists()
    {
        return Directory.Exists(Path.Combine(Application.persistentDataPath, directory));
    }
    private static string GetFullPath()
    {
        return Path.Combine(Application.persistentDataPath, directory, filename);
    }
}
