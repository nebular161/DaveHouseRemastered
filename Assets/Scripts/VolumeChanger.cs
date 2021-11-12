using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class VolumeChanger : MonoBehaviour
{
    void Start()
    {
        UpdateAudioStuff();
    }
    public void UpdateAudioStuff()
    {
        Settings settings;
        if (!SaveManager.SaveExists())
        {
            settings = new Settings();
        }
        else
        {
            settings = SaveManager.LoadSettings();
        }
        AudioSource[] sources;
        sources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in sources)
        {
            source.volume = settings.volume;
        }
    }
}
