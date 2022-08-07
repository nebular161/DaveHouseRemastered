using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Menu[] menus;
    public AudioMixer audioMixer;
    public Slider volumeSlider, sensSlider, loadingBar;

    public TMP_Text versionText;

    Resolution[] resolutions;
    Resolution selectedResolution;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Toggle postProcessingToggle;

    public string[] songList;
    public AudioClip[] songs;
    public AudioSource musicSource;

    public TMP_Dropdown musicListDropdown;

    public GameObject bossBattleButton;

    public Button updateButton;

    public TMP_Text percentageText;
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
        StartCoroutine(GetVersion());
        LoadResSettings();
        InitializeResolutionList();
        SetUpMusicList();
        fullscreenToggle.isOn = Screen.fullScreen;

        if(PlayerPrefs.GetInt("PostProcessing", 1) == 1)
        {
            postProcessingToggle.isOn = true;
        }
        else
        {
            postProcessingToggle.isOn = false;
        }

        Cursor.lockState = CursorLockMode.None;

        if(PlayerPrefs.GetInt("BossBattleUnlocked") == 1)
        {
            bossBattleButton.SetActive(true);
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
        StartCoroutine(LoadSceneAsynchronously(scene));
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
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0);
        sensSlider.value = PlayerPrefs.GetFloat("Sensitivity", 100);
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

            if (Mathf.Approximately(resolutions[i].width, selectedResolution.width) && Mathf.Approximately(resolutions[i].height, selectedResolution.height))
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
        selectedResolution = resolutions[resIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ResWidth", selectedResolution.width);
        PlayerPrefs.SetInt("ResHeight", selectedResolution.height);
    }
    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
    public void SetPostProcessing(bool thing)
    {
        if(thing)
        {
            PlayerPrefs.SetInt("PostProcessing", 1);
        }
        else
        {
            PlayerPrefs.SetInt("PostProcessing", 0);
        }
    }
    public void LoadResSettings()
    {
        selectedResolution = new Resolution();
        selectedResolution.width = PlayerPrefs.GetInt("ResWidth", Screen.currentResolution.width);
        selectedResolution.height = PlayerPrefs.GetInt("ResHeight", Screen.currentResolution.height);

        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
    public void SetUpMusicList()
    {
        musicListDropdown.ClearOptions();
        List<string> stuff = new List<string>();

        int currentMusicIndex = 0;
        for (int i = 0; i < songList.Length; i++)
        {
            stuff.Add(songList[i]);

            if (musicSource.clip == songs[i])
            {
                currentMusicIndex = i;
            }
        }

        musicListDropdown.AddOptions(stuff);
        musicListDropdown.value = currentMusicIndex;
        musicListDropdown.RefreshShownValue();
    }
    public void SetSong(int index)
    {
        musicSource.clip = songs[index];
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    IEnumerator GetVersion()
    {
        UnityWebRequest bruh = UnityWebRequest.Get("https://raw.githubusercontent.com/Moldy-Games/DaveHouseRemastered/main/versionNumber.txt");
        yield return bruh.SendWebRequest();

        if(bruh.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(bruh.error);
        }
        else
        {
            Debug.Log($"Version text downloaded: {bruh.downloadHandler.text}");
            if (bruh.downloadHandler.text != Application.version)
            {
                updateButton.gameObject.SetActive(true);
            }
        }
    }
    public void UpdateGame()
    {
        Application.OpenURL("https://gamejolt.com/games/daveshouse/713289");
        Application.Quit();
    }
    IEnumerator LoadSceneAsynchronously(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        float loadingPercentage;

        while(!operation.isDone)
        {
            loadingBar.value = operation.progress;
            loadingPercentage = operation.progress * 100;
            percentageText.text = $"{loadingPercentage.ToString("0")}%";
            yield return null;
        }
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}