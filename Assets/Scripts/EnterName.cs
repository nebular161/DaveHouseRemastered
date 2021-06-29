using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnterName : MonoBehaviour
{
    public AudioSource daveAudio;
    public TMP_InputField nameEnter;

    public AudioClip tryAgain, niceName;
    public void OnGo()
    {
        if(!string.IsNullOrEmpty(nameEnter.text))
        {
            PlayerPrefs.SetString("PlayerName", nameEnter.text);
            StartCoroutine(Continue());
        }
        else
        {
            StopAudioIfPlaying(daveAudio);
            PlayAudio(daveAudio, tryAgain);
        }
    }
    IEnumerator Continue()
    {
        nameEnter.readOnly = true;
        StopAudioIfPlaying(daveAudio);
        PlayAudio(daveAudio, niceName);

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
