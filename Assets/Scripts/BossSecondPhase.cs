using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecondPhase : MonoBehaviour
{
    bool started = false;

    public AudioSource dave;
    public AudioSource music;

    public GameObject otherDave, newDave;
    private void OnTriggerEnter(Collider other)
    {
        if(!started && other.CompareTag("Player"))
        {
            StartBoss();
        }
    }
    public void StartBoss()
    {
        started = true;
        otherDave.SetActive(false);
        newDave.SetActive(true);
        dave.Play();
        music.Play();
    }
}
