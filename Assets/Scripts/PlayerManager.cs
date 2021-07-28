using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public Collider fogTrigger, loadTrigger, daveSpeakTrigger;
    public AudioSource daveAud;
    public AudioSource music;

    public GameManager gameManager;

    public void EnableFog()
    {
        RenderSettings.fog = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!RenderSettings.fog && other == fogTrigger)
        {
            EnableFog();
        }
        else if(other == loadTrigger)
        {
            SceneManager.LoadScene("VentHalls");
        }
        else if(other == daveSpeakTrigger)
        {
            daveAud.Play();
            music.Play();
            daveSpeakTrigger.enabled = false;
            gameManager.DoLockStuff();
        }
    }
}
