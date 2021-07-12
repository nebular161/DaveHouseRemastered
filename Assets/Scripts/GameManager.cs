using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DedicatedServer;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    int points;

    public TMP_Text pointsText;


    List<int> scores = new List<int>();

    int[] _scores;

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        UpdatePointsText();
        TestPoints();
        SaveScoresToDisk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddPoints(int amount)
    {
        points += amount;
        UpdatePointsText();
    }
    public void UpdatePointsText()
    {
        pointsText.text = "Points: " + points;
    }
    public void TestPoints() // debug
    {
        int timesToAddPoints = 20;

        for (int i = 0; i < timesToAddPoints; i++)
        {
            AddPoints(UnityEngine.Random.Range(0, 2000));
        }
    }
    public void SaveScore(int score)
    {
        _scores = PlayerPrefsX.GetIntArray("Scores");
        scores.AddRange(_scores);
        scores.Add(score);
        print("Score added to list: " + score);
        SaveScoresToDisk();
    }
    public void SaveScoresToDisk()
    {
        // convert list of scores to array
        _scores = scores.ToArray();
        PlayerPrefsX.SetIntArray("Scores", _scores);

        // print stuff
        print("Score array added to PlayerPrefs");

        // sort scores
        Array.Sort(_scores);

        // convert to json data
        string sussy = JsonConvert.SerializeObject(scores);

        // print more stuff
        print(sussy);

        // generate filepath for scores
        string path = Path.Combine(Application.persistentDataPath, "scores.json");

        // check if file exists already then generate the json
        if(!File.Exists(path))
        {
            File.WriteAllText(path, sussy);

            // print even more stuff
            print("Successfully wrote score to disk");
        }
        else
        {
            File.Delete(path);
            File.WriteAllText(path, sussy);

            // how much stuff we gonna print
            print("Successfully wrote score to disk");
        }
    }
    public void EndGame()
    {
        SaveScore(points);
    }
}
