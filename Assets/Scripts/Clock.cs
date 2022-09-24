using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Clock : MonoBehaviour
{
    TMP_Text text;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        if (DateTime.Now.Minute < 10)
        {
            text.text = $"{DateTime.Now.Hour}:0{DateTime.Now.Minute}";
        }
        else
        {
            text.text = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}";
        }
    }
}
