using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundType : MonoBehaviour
{
    public soundThing soundType;

    Settings settings;

    void Update()
    {
        settings = SaveManager.LoadSettings();
        if (soundType == soundThing.Music)
        {
            GetComponent<AudioSource>().volume = settings.settingValues[0];
        }
        if(soundType == soundThing.SFX)
        {
            GetComponent<AudioSource>().volume = settings.settingValues[1];
        }
        if (soundType == soundThing.Voice)
        {
            GetComponent<AudioSource>().volume = settings.settingValues[2];
        }
    }

    public enum soundThing
    {
        Voice,
        Music,
        SFX
    }
}
