using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;
using GameJolt.API;
using GameJolt.API.Objects;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Animator daveAnim;
    public AudioSource daveAud, music, chaseMusic;
    public AudioClip houseMusic, coinAud;

    public Collider daveSpeakTrigger;

    public Door doorToLockAfterDaveSpeak;

    public int treeSpawnCount;

    public GameObject tree, treeParent, coinThing;

    public float treeMaxX, treeMinX, treeMaxZ, treeMinZ;

    public int notebooks, maxNotebooks;

    public TMP_Text notebookText;

    public GameObject daveHappy, daveAngry, secretThingOutside;

    bool musicStop;

    public bool chaseMode, finalMode;

    public ItemGuy itemGuy;
    public GameObject bayerMan, rubyMan;

    public Dave dave;
    float timeThing;
    public string gamemode;

    public float timePassed = 0;

    public TMP_Text timeText;
    public GameObject stopwatchThingies, stopwatchHorrorCharacter666;

    public float maxTimedModeTime;

    public GameObject postProcessingHandler;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        timeThing = maxTimedModeTime;
        SpawnTrees();
        UpdatePresents();

        if(DateTime.Now.Hour == 3)
        {
            Debug.Log("oh my gee its 3am");
            UnlockTrophy(162191);
            SceneManager.LoadScene("Secret");
        }
        gamemode = PlayerPrefs.GetString("Gamemode");
        if(gamemode == "Timed")
        {
            stopwatchThingies.SetActive(true);
        }
        if(PlayerPrefs.GetInt("PostProcessing", 1) == 1)
        {
            postProcessingHandler.SetActive(true);
        }
        else
        {
            postProcessingHandler.SetActive(false);
        }
    }
    void Update()
    {
        // debug bitch

        if(daveAud.isPlaying)
        {
            daveAnim.SetBool("talking", true);
        }
        else
        {
            //the only option to make this work in the audManager was to make it not destroy itself when it was done, and no other way works, WTF?
            daveAnim.SetBool("talking", false);
        }
        if(musicStop)
        {
            music.pitch -= 0.25f * Time.deltaTime;
        }
        if(gamemode == "Timed" && timeThing >= 0)
        {
            timeThing -= Time.deltaTime;
            timePassed += Time.deltaTime;
            timeText.text = timeThing.ToString("0");
        }
        if(timeThing <= 0)
        {
            timeText.text = "Times up!";
            stopwatchHorrorCharacter666.SetActive(true);
        }
    }
    public void EndGame()
    {
        SceneManager.LoadScene("Menu");
    }
    public void DoLockStuff()
    {
        if (doorToLockAfterDaveSpeak.doorOpen)
        {
            doorToLockAfterDaveSpeak.CloseDoor();
            doorToLockAfterDaveSpeak.LockDoorInfinite();
        }
        else
        {
            doorToLockAfterDaveSpeak.LockDoorInfinite();
        }
    }
    public void SpawnTrees()
    {
        for (int i = 0; i < treeSpawnCount; i++)
        {
            Vector3 thing = new Vector3(UnityEngine.Random.Range(treeMinX, treeMaxX), 12, UnityEngine.Random.Range(treeMinZ, treeMaxZ));
            Instantiate(tree, thing, tree.transform.localRotation, treeParent.transform);
        }
    }
    public void CollectPresent()
    {
        notebooks++;
        UpdatePresents();

        if(notebooks == 1)
        {
            SpawnCoin();
        }
        else if(notebooks == 2)
        {
            StartGame();
        }
        else if(notebooks == maxNotebooks)
        {
            ActivateEndMode();
        }
        if (notebooks >= 2 && dave.isActiveAndEnabled)
        {
            dave.agent.acceleration += 0.5f;
            dave.normalSpeed += 0.1f;
            dave.turnSpeed += 25;
        }
    }
    public void UpdatePresents()
    {
        notebookText.text = "Presents: " + notebooks + "/" + maxNotebooks;
    }
    public void StartGame()
    {
        if (!chaseMode)
        {
            daveHappy.SetActive(false);
            daveAngry.SetActive(true);
            rubyMan.SetActive(true);
            secretThingOutside.SetActive(false);
            StartCoroutine(StopSchoolMusic());
            itemGuy.MoveTime();
            StartCoroutine(RandomEvent.Instance.ChooseEvent());
            chaseMode = true;
        }
    }
    public void SpawnCoin()
    {
        coinThing.SetActive(true);
        daveAud.clip = coinAud;
        daveAud.Play();
    }
    IEnumerator StopSchoolMusic()
    {
        musicStop = true;
        yield return new WaitForSeconds(4);
        music.Stop();
        music.pitch = 1;
        musicStop = false;
    }
    IEnumerator FadeToRed()
    {
        yield return new WaitForSeconds(2.79f);
        Color color = RenderSettings.ambientLight;
        DOVirtual.Color(color, Color.red, 2.79f, colores =>
        {
            RenderSettings.ambientLight = colores;
        });
        finalMode = true;
    }
    public void OnEnteredHouse()
    {
        daveAud.Play();
        music.clip = houseMusic;
        music.Play();
        daveSpeakTrigger.enabled = false;
        DoLockStuff();
        UnlockTrophy(162151);
    }
    public void ActivateEndMode()
    {
        RandomEvent.Instance.StopAllEvents();
        chaseMusic.Play();
        StartCoroutine(FadeToRed());
        doorToLockAfterDaveSpeak.UnlockDoor();
    }
    public void UnlockTrophy(int id)
    {
        if(GameJoltAPI.Instance.CurrentUser != null)
        {
            Trophies.Get(id, (Trophy lol) =>
            {
                if(!lol.Unlocked)
                {
                    Trophies.Unlock(id, (bool success) =>
                    {
                        if (success)
                        {
                            Debug.Log("Trophy unlocked");
                        }
                    });
                }
            });
        }
    }
    public void SubmitScore(int score, string text, int id, string xtraData)
    {
        if(GameJoltAPI.Instance.CurrentUser != null)
        {
            Scores.Add(score, text, id, xtraData, (bool success) => {

                Debug.Log($"Score of {score} submitted successfully");
            });
        }
    }
}
