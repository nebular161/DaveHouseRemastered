using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RandomEvent : MonoBehaviour
{
    public static RandomEvent Instance; // make class a singleton

    public AudioSource source;
    public AudioClip lightsOff, fogMusic, danceMusic;
    public AudioClip[] announcements;

    public int danceTempo;

    public string[] events, eventText;

    public GameObject flashlight;

    int eventIndex, announceIndex;

    public TMP_Text eventTextObject;
    public GameObject eventBox;

    public string currentEvent;

    public Material blackSkybox, normalSkybox;

    [SerializeField] float minTime, maxTime;

    public Look look;
    private void Awake()
    {
        Instance = this;
    }
    public IEnumerator ChooseEvent()
    {
        currentEvent = "Choosing";
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        announceIndex = Random.Range(0, announcements.Length);
        source.PlayOneShot(announcements[announceIndex]);
        yield return new WaitForSeconds(announcements[announceIndex].length);
        eventIndex = Random.Range(0, events.Length);
        StartCoroutine(events[eventIndex]);
        StartCoroutine(ShowEventText(eventText[eventIndex]));
        currentEvent = events[eventIndex];
    }
    IEnumerator ShowEventText(string text)
    {
        eventBox.SetActive(true);
        eventTextObject.text = text;
        yield return new WaitForSeconds(4);
        eventBox.SetActive(false);
    }
    IEnumerator LightsOut()
    {
        source.PlayOneShot(lightsOff);
        RenderSettings.ambientLight = Color.black;
        RenderSettings.skybox = blackSkybox;
        flashlight.SetActive(true);
        yield return new WaitForSeconds(Random.Range(45f, 60f));
        RenderSettings.ambientLight = Color.white;
        RenderSettings.skybox = normalSkybox;
        flashlight.SetActive(false);
        StartCoroutine(ChooseEvent());
    }
    IEnumerator FogMode()
    {
        source.PlayOneShot(fogMusic);
        DOVirtual.Float(0f, 0.04f, 2, bruh =>
        {
            RenderSettings.fogDensity = bruh;
        });
        yield return new WaitForSeconds(Random.Range(60f, 80f));
        DOVirtual.Float(0.04f, 0f, 2, bruh =>
        {
            RenderSettings.fogDensity = bruh;
        });
        StartCoroutine(ChooseEvent());
    }
    IEnumerator UpsideDown()
    {
        DOVirtual.Float(0, 180, 1, theValue =>
        {
            look.flipValue = theValue;
        });
        yield return new WaitForSeconds(Random.Range(40f, 60f));
        DOVirtual.Float(180, 0, 1, theValue =>
        {
            look.flipValue = theValue;
        });
        StartCoroutine(ChooseEvent());
    }
    public void StopAllEvents()
    {
        if(currentEvent == "Choosing")
        {
            StopCoroutine(ChooseEvent());
        }
        else if(currentEvent == "LightsOut")
        {
            StopCoroutine(LightsOut());
            RenderSettings.ambientLight = Color.white;
            RenderSettings.skybox = normalSkybox;
            flashlight.SetActive(false);       
        }
        else if(currentEvent == "FogMode")
        {
            StopCoroutine(FogMode());
            source.Stop();
            DOVirtual.Float(0.04f, 0f, 2, bruh =>
            {
                RenderSettings.fogDensity = bruh;
            });
        }
        else if(currentEvent == "UpsideDown")
        {
            StopCoroutine(UpsideDown());
            DOVirtual.Float(180, 0, 1, theValue =>
            {
                look.flipValue = theValue;
            });
        }
    }
}
