using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RubyMan : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    [SerializeField] float forceShowTime, anger;

    public bool gettingAngry, angry, debug;

    AudioSource audioSource;
    public SpriteRenderer spriteRenderer;

    public AudioClip intro, loop;
    public Sprite angryRuby;
    public GameObject sprite;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        sprite.gameObject.SetActive(false);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if(GameManager.Instance.notebooks >= 2 || debug)
        {
            Vector3 direction = player.position - transform.position;
            RaycastHit raycastHit;
            if(Physics.Raycast(transform.position + Vector3.up * 2f, direction, out raycastHit) & raycastHit.transform == player & spriteRenderer.isVisible & sprite.activeSelf)
            {
                gettingAngry = true;
            }
            else
            {
                gettingAngry = false;
            }
        }
    }
    void Update()
    {
        if(forceShowTime > 0)
        {
            forceShowTime -= Time.deltaTime;
        }
        if(gettingAngry)
        {
            anger += Time.deltaTime;
            if(anger >= 1 && !angry)
            {
                angry = true;
                audioSource.PlayOneShot(intro);
                spriteRenderer.sprite = angryRuby;
            }
        }
        else if(anger > 0)
        {
            anger -= Time.deltaTime;
        }
        if(!angry)
        {
            if(((transform.position - agent.destination).magnitude <= 20 & (transform.position - player.position).magnitude >= 60) || forceShowTime > 0)
            {
                spriteRenderer.gameObject.SetActive(true);
            }
            else
            {
                spriteRenderer.gameObject.SetActive(false);
            }
        }
        else
        {
            agent.speed += 60f * Time.deltaTime;
            TargetPlayer();
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(loop);
            }
        }
    }
    public void TargetPlayer()
    {
        agent.SetDestination(player.position);
    }
    public void DoSometingLocationRelated(Vector3 location, bool flee)
    {
        if(!angry & agent.isActiveAndEnabled)
        {
            agent.SetDestination(location);
            if(flee)
            {
                forceShowTime = 3;
            }
        }
    }
}
