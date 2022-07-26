using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Spike : MonoBehaviour
{
    Transform player;
    AudioSource source;

    public AudioClip alert, fire;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        source = GetComponent<AudioSource>();
        StartCoroutine(StartSpike());
    }
    IEnumerator StartSpike()
    {
        Vector3 goHere = player.position;
        transform.position = new Vector3(goHere.x, -3f, goHere.z);
        source.PlayOneShot(alert);
        yield return new WaitForSeconds(Random.Range(0.8f, 1f));
        source.PlayOneShot(fire);
        transform.DOMoveY(2.25f, 0.2f);
        yield return new WaitForSeconds(1);
        transform.DOMoveY(-2.5f, 0.2f);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
