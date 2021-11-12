using UnityEngine;
using System.IO;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderSaving : MonoBehaviour
{
    public enum SaveType
    {
        Volume,
        Sensitivity
    }

    public SaveType saveType;

    void Start()
    {
        if (!SaveManager.SaveExists())
        {
            Settings settings = new Settings();
            SaveManager.SaveSettings(settings);
        }
        else
        {
            LoadSettings();
        }
    }
    public void SaveSettings()
    {
        Settings settings = new Settings();

        if(saveType == SaveType.Volume)
        {
            settings.volume = GetComponent<Slider>().value;
        }
        else if(saveType == SaveType.Sensitivity)
        {
            settings.sens = GetComponent<Slider>().value;
        }

        SaveManager.SaveSettings(settings);
    }
    public void LoadSettings()
    {
        Settings settings = SaveManager.LoadSettings();

        if (saveType == SaveType.Volume)
        {
            GetComponent<Slider>().value = settings.volume;
        }
        else if (saveType == SaveType.Sensitivity)
        {
            GetComponent<Slider>().value = settings.sens;
        }
    }
}
