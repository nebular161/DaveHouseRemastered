using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static string directory = PlayerPrefs.GetString("PlayerName");
    public static string filename = "settings.davedata";

	public const byte LatestSaveVersion = 0;

	public static void SaveSettings(Settings settings)
    {
        FileStream fs = File.Open(GetFullPath(),FileMode.OpenOrCreate);
		BinaryWriter br = new BinaryWriter(fs);
		br.Write((byte)settings.version);
		br.Write(settings.volume);
		br.Write(settings.sens);
		br.Write(settings.hasWon);
		
		fs.Close();
    }
    public static Settings LoadSettings()
    {
        if(SaveExists())
        {
            try
            {
                FileStream fs = File.Open(GetFullPath(), FileMode.OpenOrCreate);
				BinaryReader br = new BinaryReader(fs);
				Settings settings = new Settings();
				settings.version = br.ReadByte();
				if (settings.version != LatestSaveVersion)
				{
					switch (settings.version) //implement legacy reading for old file versiolns and then resave the file or whatever, rn though 0 is the only version
					{
						default:
							throw new System.Exception("Save File out of date!");
					}
				}
				else
				{
					settings.version = LatestSaveVersion;
					settings.volume = br.ReadSingle();
					settings.sens = br.ReadSingle();
					settings.hasWon = br.ReadBoolean();
				}
				Debug.Log(settings.version);
				Debug.Log(settings.volume);
				Debug.Log(settings.sens);
				Debug.Log(settings.hasWon);
				fs.Close();

                return settings;
            }
            catch(SerializationException ex)
            {
                Debug.LogWarning("File loading failed: " + ex);
                SceneManager.LoadScene("FileCorrupt");
            }
        }

        return null;
    }
    public static bool SaveExists()
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
