using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    bool paused;
    void Start()
    {
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        if(paused)
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                paused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
