using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DedicatedServer;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Animator daveAnim;
    public AudioSource daveAud, music;

    public Door doorToLockAfterDaveSpeak;

    public int treeSpawnCount;

    public GameObject tree, treeParent;

    public float treeMaxX, treeMinX, treeMaxZ, treeMinZ;

    int notebooks;

    public TMP_Text notebookText;

    public GameObject daveHappy, daveAngry;

    bool musicStop;

    public bool chaseMode;

    public SpriteRenderer[] presentSprites;
    public Material presentMat;

    void Start()
    {
        SpawnTrees();
        UpdatePresents();
        RandomizeNotebookColors();
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
            StartGame();
        }
        else if(notebooks == 10)
        {
            doorToLockAfterDaveSpeak.UnlockDoor();
        }
    }
    public void UpdatePresents()
    {
        notebookText.text = "Presents: " + notebooks + "/10";
    }
    public void StartGame()
    {
        if (!chaseMode)
        {
            daveHappy.SetActive(false);
            daveAngry.SetActive(true);
            StartCoroutine(StopSchoolMusic());
            chaseMode = true;
        }
    }
    IEnumerator StopSchoolMusic()
    {
        musicStop = true;
        yield return new WaitForSeconds(4);
        music.Stop();
        music.pitch = 1;
        musicStop = false;
    }
    public void RandomizeNotebookColors()
    {
        foreach(Renderer renderer in presentSprites)
        {
            renderer.material.SetVector("_HSVAAdjust", new Vector4(UnityEngine.Random.Range(0f, 1f), 0, 0, 0));
        }
    }
}
