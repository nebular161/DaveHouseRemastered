using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJolt.UI;
using GameJolt.API;

public class GJScreen : MonoBehaviour
{
    public bool signedIn;
    void Start()
    {
        GameJoltUI.Instance.ShowSignIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameJoltAPI.Instance.CurrentUser != null)
        {
            signedIn = true;
        }
    }
    public void TestTrophy()
    {
        Trophies.Unlock(162071, (bool success) =>
        {
            if (success)
            {
                Debug.Log("cool it works");
            }
            else
            {
                Debug.LogWarning("dummy, fix your code now!");
            }
        });
    }
}
