using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameJolt.UI;
using GameJolt.API;
using GameJolt.API.Objects;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GJScreen : MonoBehaviour
{
    public TMP_InputField username, gameToken;
    public TMP_Text textUsername;
    public Image avatar;

    Action<bool> callback;
    public void LogIn()
    {
        if(username.text.Trim() == string.Empty || gameToken.text.Trim() == string.Empty)
        {
            Debug.Log("lol empty credentials");
        }
        else
        {
            User user = new User(username.text.Trim(), gameToken.text.Trim());
            user.SignIn((bool success) =>
            {
               if (success)
               {
                   Dismiss(true);
               }
            });
            MenuManager.Instance.OpenMenu("Title");
        }
    }
    public void LogOut()
    {
        GameJoltAPI.Instance.CurrentUser.SignOut();
        MenuManager.Instance.OpenMenu("Title");
    }
    public void Dismiss(bool success)
    {
        if(callback != null)
        {
            callback(success);
            callback = null;
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
    public void GoToGameJoltMenu()
    {
        if (GameJoltAPI.Instance.CurrentUser == null)
        {
            MenuManager.Instance.OpenMenu("SignInScreen");
        }
        else
        {
            MenuManager.Instance.OpenMenu("LoggedInScreen");

            if (avatar != GameJoltAPI.Instance.CurrentUser.Avatar)
            {
                GameJoltAPI.Instance.CurrentUser.DownloadAvatar();
                avatar.sprite = GameJoltAPI.Instance.CurrentUser.Avatar;
            }
            textUsername.text = GameJoltAPI.Instance.CurrentUser.Name;
        }
    }
}
