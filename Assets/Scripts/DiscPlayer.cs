using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class DiscPlayer : MonoBehaviour
{
    public static DiscPlayer Instance;
    public VideoPlayer player;

    public MeshRenderer screen;
    public Material videoPlaying, black;

    public AudioSource videoAudio;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if(player.isPlaying)
        {
            screen.material = videoPlaying;
        }
        else
        {
            screen.material = black;
        }
    }
    public void GetClip()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Videos");
        string[] videos = Directory.GetFiles(path, "*.mp4");
        player.source = VideoSource.Url;
        player.url = videos[Random.Range(0, videos.Length)];
        GameManager.Instance.UnlockTrophy(169770);
        StartCoroutine(PlayVideo());
    }
    private IEnumerator PlayVideo()
    {
        player.audioOutputMode = VideoAudioOutputMode.AudioSource;
        player.controlledAudioTrackCount = 1;
        player.EnableAudioTrack(0, true);
        player.SetTargetAudioSource(0, videoAudio);

        player.Prepare();
        while(!player.isPrepared)
        {
            yield return null;
        }

        player.Play();

        while(player.isPlaying)
        {
            yield return null;
        }
    }
}
