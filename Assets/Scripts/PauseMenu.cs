using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    bool paused;

    public TMP_Text pauseText;

    public PlayerManager playerManager;
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
        if(RandomEvent.Instance.currentEvent == "LightsOut" || playerManager.inPadGame)
        {
            pauseText.color = Color.white;
        }
        else
        {
            pauseText.color = Color.black;
        }
    }
}
