using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSliders : MonoBehaviour
{
    public string playerPrefsName;
    void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat(playerPrefsName);
    }

    public void SetSoundTypeVolume()
    {
        PlayerPrefs.SetFloat(playerPrefsName, GetComponent<Slider>().value);
    }
}
