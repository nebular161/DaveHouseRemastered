using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Newtonsoft.Json;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Menu[] menus;

    public TMP_Text scoreText;

    public Slider musicSlider, sfxSlider, voicesSlider;

    Settings settings;

    private void Awake()
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
        Cursor.lockState = CursorLockMode.None;

        string path = Path.Combine(Application.persistentDataPath, PlayerPrefs.GetString("PlayerName"), "menuSettings.json");
        string rawJson = File.ReadAllText(path);

        Settings _settings = JsonConvert.DeserializeObject<Settings>(rawJson);

        if (File.Exists(path))
        {
            musicSlider.value = _settings.music;
            sfxSlider.value = _settings.sfx;
            voicesSlider.value = _settings.voices;
        }
        else
        {
            musicSlider.value = 0.5f;
            sfxSlider.value = 0.5f;
            voicesSlider.value = 0.5f;
        }
    }
    private void Update()
    {
        SaveSettingsForUser();
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
        SceneManager.LoadScene(scene);
    }
    public void DeleteData()
    {
        string path = Path.Combine(Application.persistentDataPath, PlayerPrefs.GetString("PlayerName"));
        string[] files = Directory.GetFiles(path);

        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }

        Directory.Delete(path);
        LoadScene("NameEnter");
    }
    public void SaveSettingsForUser()
    {
        settings = new Settings();

        settings.music = musicSlider.value;
        settings.sfx = sfxSlider.value;
        settings.voices = voicesSlider.value;

        string path = Path.Combine(Application.persistentDataPath, PlayerPrefs.GetString("PlayerName"), "menuSettings.json");
        string serializedSettings = JsonConvert.SerializeObject(settings);

        File.WriteAllText(path, serializedSettings);
    }
}
public class Settings
{
    public float music;
    public float sfx;
    public float voices;
    public float mouseSens;
}