using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;

    void Start()
    {
        discord = new Discord.Discord(866036324741021706, (System.UInt64)CreateFlags.Default);
    }
    void Update()
    {
        discord.RunCallbacks();
    }
    public void CreateRPC(string details, string state)
    {
        ActivityManager activityManager = discord.GetActivityManager();
        Activity activity = new Activity
        {
            Details = details,
            State = state      
        };
        activityManager.UpdateActivity(activity, (res) => { 
            if(res == Discord.Result.Ok)
            {
                print("status set");
            }
            else
            {
                print("status update failed");
            }
        });
    }
    private void OnApplicationQuit()
    {
        discord.Dispose();
    }
}
