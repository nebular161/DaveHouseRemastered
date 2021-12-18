using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class FileCorruptScreen : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            string path = Path.Combine(Application.persistentDataPath, PlayerPrefs.GetString("PlayerName"));
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(files[i]);
            }

            Directory.Delete(path);
            SceneManager.LoadScene("NameEnter");
        }
    }
}
