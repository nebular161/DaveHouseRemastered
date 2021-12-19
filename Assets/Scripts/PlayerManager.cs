using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public Collider fogTrigger, loadTrigger, daveSpeakTrigger, unFogTrigger;
    public AudioSource daveAud;
    public AudioSource music;

    public GameManager gameManager;
    public ItemManager itmManager;

    public bool inCaves;

    bool loadingAScene;

    public static bool transitioning = false;

    public AudioClip daveTalk, musicAud;

    private void Awake()
    {
        if (transitioning) transitioning = false;
    }

    private void Start()
    {
        
    }

    public void EnableFog()
    {
        RenderSettings.fog = true;
        StartCoroutine(smoothFog());

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == daveSpeakTrigger)
        {
            daveAud.clip = daveTalk;
            music.clip = musicAud;
            daveAud.Play();
            music.Play();
            music = GameObject.Find("Music").GetComponent<AudioSource>();
            daveAud = GameObject.Find("DaveIntroTalk").GetComponent<AudioSource>();
            gameManager.daveAud = daveAud;
            gameManager.music = music;
            daveSpeakTrigger.enabled = false;
            gameManager.DoLockStuff();
        }
    }

    IEnumerator smoothFog()
    {
        RenderSettings.fogDensity = 0;
        while(RenderSettings.fogDensity < 0.1)
        {
            RenderSettings.fogDensity += 0.03f * Time.deltaTime;
            yield return null;
        }
    }

  
}
