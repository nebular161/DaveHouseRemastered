using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

// CODE WRITTEN BY BADRUM55

public class MathMachine : MonoBehaviour
{
    public static Problem math;

    public static int solution;
    public enum Sign
    {
        Plus,
        Minus
    }
    public static int[] num;

    Sign _sign;

    [Serializable]
    public struct Problem
    { 
        public string ProblemString(int[] num, Sign sign)
        {
            return num[0].ToString() + ((sign == Sign.Plus) ? "+" : "-") + num[1].ToString();
        }

    }
    private void Start()
    {
        // randomize numbers
        num = new int[]
        {
            UnityEngine.Random.Range(0, 9),
            UnityEngine.Random.Range(0, 9)
        };

        // get text object
        TMP_Text text = GetComponentInChildren<TMP_Text>();

        text.text = math.ProblemString(num, _sign);
    }

}
