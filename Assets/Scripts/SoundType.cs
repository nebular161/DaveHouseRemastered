using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[RequireComponent(typeof(AudioSource))]
public class SoundType : MonoBehaviour
{
    public soundThing soundType;
    string path, rawJson;

    Settings settings;

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, PlayerPrefs.GetString("PlayerName"), "menuSettings.json");
        rawJson = File.ReadAllText(path);

        settings = JsonConvert.DeserializeObject<Settings>(rawJson);

    }
    void Update()
    {
        if(soundType == soundThing.Music)
        {
            GetComponent<AudioSource>().volume = settings.music;
        }
        if(soundType == soundThing.SFX)
        {
            GetComponent<AudioSource>().volume = settings.sfx;
        }
        if (soundType == soundThing.Voice)
        {
            GetComponent<AudioSource>().volume = settings.voices;
        }
    }

    public enum soundThing
    {
        Voice,
        Music,
        SFX
    }
}
