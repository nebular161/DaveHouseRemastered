using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Menu[] menus;
    public AudioMixer audioMixer;
    public Slider volumeSlider, sensSlider;

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
}