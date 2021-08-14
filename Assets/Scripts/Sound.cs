using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public SoundType soundType;

    public AudioClip clip;

    public float volume;

    public string name;

    public bool Loop;

    public bool PlayOnAwake;

    public bool destroyWhenFinished = true;
}
