using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    Settings settings;

    public Sound[] sounds;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        settings = SaveManager.LoadSettings();

        foreach (Sound s in sounds)
        {
            switch (s.soundType)
            {
                case SoundType.MUSIC:
                    s.volume = settings.settingValues[0];
                    break;
                case SoundType.SFX:
                    s.volume = settings.settingValues[1];
                    break;
                case SoundType.VOICES:
                    s.volume = settings.settingValues[2];
                    break;
            }

            if (s.PlayOnAwake)
            {
                PlaySound(s.name, null, 0);
            }
        }
    }

    private void Update()
    {
    }

    public void PlaySound(string name, AudioSource source, float delayValue)
    {
        foreach(Sound sound in sounds)
        {
            if(sound.name == name)
            {
                if (!source)
                {
                    GameObject newAud = new GameObject(name, typeof(AudioSource));
                    newAud.GetComponent<AudioSource>().clip = sound.clip;
                    newAud.GetComponent<AudioSource>().playOnAwake = false;
                    newAud.GetComponent<AudioSource>().loop = sound.Loop;
                    newAud.GetComponent<AudioSource>().PlayDelayed(delayValue);
                    newAud.GetComponent<AudioSource>().volume = sound.volume;
                    if (sound.destroyWhenFinished)
                    {
                        Destroy(newAud, sound.clip.length);
                    }
                }
                else
                {
                    source.clip = sound.clip;
                    source.playOnAwake = false;
                    source.loop = sound.Loop;
                    source.volume = sound.volume;
                    source.PlayDelayed(delayValue);
                }
            }
        }
    }
}
