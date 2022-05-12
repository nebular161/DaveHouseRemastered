using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Menu[] menus;
    public AudioMixer audioMixer;
    public Slider volumeSlider, sensSlider;

    public TMP_Text versionText;

    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        if(Instance != this)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        versionText.text = $"v{Application.version}";
        InitializeResolutionList();
        Cursor.lockState = CursorLockMode.None;

        if(PlayerPrefs.GetFloat("Sensitivity") < 50)
        {
            PlayerPrefs.SetFloat("Sensitivity", 100);
        }

        SetSliderValues();
    }
    public void OpenMenu(string name)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if(menus[i].menuName == name)
            {
                OpenMenu(menus[i]);
            }
            else if(menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }
    public void OpenMenu(Menu menu)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if(menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
    public void LoadScene(string scene)
    {
        OpenMenu("Loading");
        SceneManager.LoadScene(scene);
    }
    public void ChangeVolume()
    {
        audioMixer.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
    public void ChangeSensitivity()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensSlider.value);
    }
    public void SetSliderValues()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        sensSlider.value = PlayerPrefs.GetFloat("Sensitivity");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetGameMode(string gamemode)
    {
        PlayerPrefs.SetString("Gamemode", gamemode);
    }
    public void InitializeResolutionList()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}