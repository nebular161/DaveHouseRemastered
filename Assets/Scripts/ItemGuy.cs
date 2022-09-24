using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGuy : MonoBehaviour
{
    public float minTime, maxTime;
    [SerializeField] bool alreadyPlayedAttentionGrabber;
    public Transform player;

    AudioSource soundPlayer;
    public AudioClip[] giveSounds;
    public AudioClip attentionGrabberSound, scamSound, finalMode;

    private void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }
    public void MoveTime()
    {
        StartCoroutine(GetAMoveOn());
    }
    IEnumerator GetAMoveOn()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        Transform spotToGoTo = WanderPoints.Instance.GetWanderPoint();
        transform.position = new Vector3(spotToGoTo.position.x, 5, spotToGoTo.position.z);
        alreadyPlayedAttentionGrabber = false;
        StartCoroutine(GetAMoveOn());
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= 20 && !alreadyPlayedAttentionGrabber)
        {
            soundPlayer.clip = attentionGrabberSound;
            soundPlayer.Play();
            alreadyPlayedAttentionGrabber = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!GameManager.Instance.finalMode)
            {
                int theScamValue = Random.Range(0, 5);
                bool scammed = theScamValue == 3;

                if (scammed)
                {
                    if (ItemManager.Instance.items[0] == 0 && ItemManager.Instance.items[1] == 0 && ItemManager.Instance.items[2] == 0)
                    {
                        GiveItem();
                    }
                    else
                    {
                        TakeItem();
                    }
                }
                else
                {
                    GiveItem();
                }
            }
            else
            {
                for (int i = 0; i < ItemManager.Instance.items.Length; i++)
                {
                    ItemManager.Instance.items[i] = 0;
                }
                soundPlayer.clip = finalMode;
                soundPlayer.Play();
            }
        }
    }
    public void GiveItem()
    {
        GameManager.Instance.UnlockTrophy(162194);
        soundPlayer.clip = giveSounds[Random.Range(0, giveSounds.Length)];
        soundPlayer.Play();
        ItemManager.Instance.DropItem(ItemManager.Instance.items[ItemManager.Instance.selectedItem]);
        ItemManager.Instance.ReplaceItem(ItemManager.Instance.selectedItem, Random.Range(1, ItemManager.Instance.itemDrop.Length));
        transform.position = new Vector3(transform.position.x, -45, transform.position.z);
        alreadyPlayedAttentionGrabber = false;
    }
    public void TakeItem()
    {
        GameManager.Instance.UnlockTrophy(173544);
        int theNumberOne = Random.Range(0, 2);
        while (ItemManager.Instance.items[theNumberOne] == 0)
        {
            theNumberOne = Random.Range(0, 2);
        }
        ItemManager.Instance.ReplaceItem(theNumberOne, 0);
        soundPlayer.clip = scamSound;
        soundPlayer.Play();
        transform.position = new Vector3(transform.position.x, -45, transform.position.z);
        alreadyPlayedAttentionGrabber = false;
    }

    // this code is terrible stop looking at this!
}
