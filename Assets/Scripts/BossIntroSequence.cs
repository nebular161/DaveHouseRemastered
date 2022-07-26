using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class BossIntroSequence : MonoBehaviour
{
    bool stopAmbience, sequenceStarted;
    public AudioSource daveTalk, ambience, bossMusic;
    public AudioClip daveIntro, weirdCreepySlide, ready;

    public Transform daveThing;

    public DaveBoss boss;

    public GameObject healthBars;
    void Start()
    {
        
    }
    void Update()
    {
        if(stopAmbience)
        {
            ambience.volume -= 0.25f * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(BossSequence());
    }
    IEnumerator BossSequence()
    {
        if(!sequenceStarted)
        {
            sequenceStarted = true;
            stopAmbience = true;
            yield return new WaitForSeconds(5);
            stopAmbience = false;
            daveTalk.PlayOneShot(daveIntro);
            yield return new WaitForSeconds(37);
            daveTalk.PlayOneShot(weirdCreepySlide);
            daveThing.DOMoveY(0, 6.85f);
            yield return new WaitForSeconds(9);
            daveTalk.PlayOneShot(ready);
            yield return new WaitForSeconds(5);
            bossMusic.Play();
            healthBars.SetActive(true);
            StartCoroutine(boss.RepeatAttacks());
        }
    }
}
