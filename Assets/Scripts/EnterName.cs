using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class EnterName : MonoBehaviour
{
    public AudioSource daveAudio;
    public TMP_InputField nameEnter;

    public AudioClip tryAgain, niceName, welcomeBack;
    public void OnGo()
    {
        if (Directory.Exists(Path.Combine(Application.persistentDataPath, nameEnter.text)))
        {
            PlayerPrefs.SetString("PlayerName", nameEnter.text);
            StartCoroutine(Continue(welcomeBack));
        }
        if (!string.IsNullOrEmpty(nameEnter.text))
        {
            PlayerPrefs.SetString("PlayerName", nameEnter.text);
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, PlayerPrefs.GetString("PlayerName")));
            StartCoroutine(Continue(niceName));
        }
        else
        {
            StopAudioIfPlaying(daveAudio);
            PlayAudio(daveAudio, tryAgain);
        }
    }
    IEnumerator Continue(AudioClip clip)
    {
        nameEnter.readOnly = true;
        StopAudioIfPlaying(daveAudio);
        PlayAudio(daveAudio, clip);

        yield return new WaitForSeconds(niceName.length);
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
}
