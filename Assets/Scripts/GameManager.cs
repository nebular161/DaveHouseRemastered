using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DedicatedServer;
using TMPro;
using Newtonsoft.Json;
using System.IO;

public class GameManager : MonoBehaviour
{
    int points;

    public TMP_Text pointsText;


    int[] scores = new int[]
    {
        8384,
        234234,
        2343,
        3754
    };

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        UpdatePointsText();
        TestPoints();
        SaveScores();
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
            AddPoints(Random.Range(0, 2000));
        }
    }
    public void SaveScores()
    {
        string sussy = JsonConvert.SerializeObject(scores);
        Debug.Log(sussy);

        string path = Path.Combine(Application.persistentDataPath, "scores.json");

        File.WriteAllText(path, sussy);
    }
}
