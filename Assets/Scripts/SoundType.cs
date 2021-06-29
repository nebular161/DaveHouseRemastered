using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundType : MonoBehaviour
{
    public soundThing soundType;

    void Update()
    {
        if(soundType == soundThing.Music)
        {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
        }
        if(soundType == soundThing.SFX)
        {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
        }
        if (soundType == soundThing.Voice)
        {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("VoiceVolume");
        }
    }

    public enum soundThing
    {
        Voice,
        Music,
        SFX
    }
}
