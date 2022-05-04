using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour
{
    public Collider player;
    public AudioClip youGotMail;
    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if(other == player)
        {
            GameManager.Instance.UnlockTrophy(162160);
            audioSource.PlayOneShot(youGotMail);
        }
    }
}
