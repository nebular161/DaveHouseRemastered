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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // debug bitch
        if(Input.GetKeyDown(KeyCode.F5))
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
