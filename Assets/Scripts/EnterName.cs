using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class EnterName : MonoBehaviour
{
    public AudioSource daveAudio;
    public TMP_InputField nameEnter;

    public AudioClip tryAgain, niceName, welcomeBack;

    public TMP_Text saves;

    List<string> dirs = new List<string>();

    private void Start()
    {
        ShowSaves();
    }
    public void OnGo()
    {
        if (Directory.Exists(Path.Combine(Application.persistentDataPath, nameEnter.text)))
        {
            print("Save found: " + nameEnter.text);
            PlayerPrefs.SetString("PlayerName", nameEnter.text);
            StartCoroutine(Continue(welcomeBack));
        }
        else if (!Directory.Exists(Path.Combine(Application.persistentDataPath, nameEnter.text)))
        {
            print("Creating save file");
            PlayerPrefs.SetString("PlayerName", nameEnter.text);
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, PlayerPrefs.GetString("PlayerName")));
            StartCoroutine(Continue(niceName));
        }
        else if(string.IsNullOrEmpty(nameEnter.text))
        {
            print("Name entered null");
            StopAudioIfPlaying(daveAudio);
            PlayAudio(daveAudio, tryAgain);
        }
        else
        {
            print("what the hell do i do");
        }
    }
    IEnumerator Continue(AudioClip clip)
    {
        nameEnter.readOnly = true;
        StopAudioIfPlaying(daveAudio);
        PlayAudio(daveAudio, clip);

        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene("Menu");
    }
    public void StopAudioIfPlaying(AudioSource source)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
    }
    public void PlayAudio(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    public void ShowSaves()
    {
        DirectoryInfo e = new DirectoryInfo(Application.persistentDataPath);
        DirectoryInfo[] stuff = e.GetDirectories();

        foreach (DirectoryInfo bruh in stuff)
        {
            dirs.Add(bruh.Name);
        }
        for (int i = 0; i < dirs.Count; i++)
        {
            saves.text += dirs[i] + "\n";
        }
    }
}
