using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFallingTrigger : MonoBehaviour
{
    public Transform player;
    bool inTrigger;

    public GameObject spikeTall;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inTrigger = true;
            StartCoroutine(FallSpikes());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inTrigger = false;
        }
    }
    IEnumerator FallSpikes()
    {
        yield return new WaitForSeconds(1);
        SpikeFall();
    }
    public void SpikeFall()
    {
        if(inTrigger)
        {
            Vector3 pos = player.position;
            pos.y = -30;
            Instantiate(spikeTall, pos, spikeTall.transform.rotation);
            StartCoroutine(FallSpikes());
        }
    }
}
