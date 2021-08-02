using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

[RequireComponent(typeof(Slider))]
public class VolumeSliders : MonoBehaviour
{
    public bool music, sfx, voices;
    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, PlayerPrefs.GetString("PlayerName"), "menuSettings.json");
        string rawJson = File.ReadAllText(path);

        Settings _settings = JsonConvert.DeserializeObject<Settings>(rawJson);

        if(music)
        {
            GetComponent<Slider>().value = _settings.music;
        }
    }
}
