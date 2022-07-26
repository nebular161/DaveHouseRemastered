using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DaveBoss : MonoBehaviour
{
    public GameObject pyramid, player, direction, ventExit, spike;
    public SpriteRenderer daveSprite;
    public Sprite idle, attacking, preparingAttackPyramid, defeated;

    public AudioSource daveAudio, music;
    public AudioClip throwSound, defeatedClip;

    int timesToThrow, timesToSpawnSpike;
    float health = 500;
    public Slider healthBar;

    public string[] attacks;

    public bool bossStarted, bossDefeated, bossDefeatStarted, stopMusic;
    private void Update()
    {
        direction.transform.LookAt(player.transform);
        healthBar.value = health;

        if(health <= 0)
        {
            bossDefeated = true;
        }
        if(stopMusic)
        {
            music.volume -= 0.25f * Time.deltaTime;
        }
        if(!bossDefeatStarted && bossDefeated)
        {
            StopAllCoroutines();
            StartCoroutine(Defeated());
            bossDefeatStarted = true;
        }
    }
    public IEnumerator RepeatAttacks()
    {
        if(!bossDefeated)
        {
            bossStarted = true;
            timesToThrow = Random.Range(3, 6);
            timesToSpawnSpike = Random.Range(5, 7);
            yield return new WaitForSeconds(Random.Range(4f, 7f));
            int attackSelection = Random.Range(0, attacks.Length);
            Debug.Log($"Attack selected: {attacks[attackSelection]}");
            StartCoroutine(attacks[attackSelection]);
        }
    }
    IEnumerator PyramidAttack()
    {
        yield return new WaitForSeconds(1.5f);
        if(timesToThrow > 0)
        {
            StartCoroutine(ThrowPyramid());
        }
        else
        {
            daveSprite.sprite = idle;
            StartCoroutine(RepeatAttacks());
        }
    }
    IEnumerator SpikeAttack()
    {
        yield return new WaitForSeconds(1.5f);
        if (timesToSpawnSpike > 0)
        {
            SpawnSpike();
        }
        else
        {
            StartCoroutine(RepeatAttacks());
        }
    }
    IEnumerator ThrowPyramid()
    {
        timesToThrow--;
        daveSprite.sprite = preparingAttackPyramid;
        yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        Instantiate(pyramid, direction.transform.position, direction.transform.rotation);
        daveAudio.PlayOneShot(throwSound);
        daveSprite.sprite = attacking;
        StartCoroutine(PyramidAttack());
    }
    public void SpawnSpike()
    {
        timesToSpawnSpike--;
        Instantiate(spike);
        StartCoroutine(SpikeAttack());
    }
    IEnumerator Defeated()
    {
        ventExit.SetActive(false);
        stopMusic = true;
        daveAudio.PlayOneShot(defeatedClip);
        daveSprite.sprite = defeated;
        yield return new WaitForSeconds(8);
        transform.DOMoveY(-45, 8);
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LaserShot") && bossStarted)
        {
            health -= Random.Range(1f, 3f);
            Destroy(other);
        }
    }
}
