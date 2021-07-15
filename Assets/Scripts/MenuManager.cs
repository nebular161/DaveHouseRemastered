using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Menu[] menus;

    public TMP_Text scoreText;

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
    public void GetScores()
    {
        List<int> scores = new List<int>();
        scores.Sort();
        scores.AddRange(PlayerPrefsX.GetIntArray("Scores"));

        for (int i = 0; i < scores.Count; i++)
        {
            scoreText.text += scores[i] + "\n";

            if(i > 10)
            {
                return;
            }
        }
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
}
