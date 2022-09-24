using UnityEngine;

public class StopwatchPoster : MonoBehaviour
{
    public Material stopwatchPoster, wall;
    public MeshRenderer rendererThing;
    void Start()
    {
        if(PlayerPrefs.GetString("Gamemode") == "Timed")
        {
            rendererThing.material = stopwatchPoster;
        }
        else
        {
            rendererThing.material = wall;
        }
    }
}
