using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DedicatedServer;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
    public GameObject bayerMan;

    public Dave dave;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SpawnTrees();
        UpdatePresents();
    }
    void Update()
    {
        // debug bitch
        if(Input.GetKeyDown(KeyCode.F5))
        {
            EndGame();
        }

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
            dave.normalSpeed += 0.5f;
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
            secretThingOutside.SetActive(false);
            StartCoroutine(StopSchoolMusic());
            itemGuy.MoveTime();
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
    }
    public void ActivateEndMode()
    {
        chaseMusic.Play();
        StartCoroutine(FadeToRed());
        doorToLockAfterDaveSpeak.UnlockDoor();
    }
}
