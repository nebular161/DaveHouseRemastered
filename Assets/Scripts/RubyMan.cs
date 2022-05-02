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

    Color intial;

    public Look look;
    void Start()
    {
        intial = RenderSettings.ambientLight;
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        sprite.gameObject.SetActive(false);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if(GameManager.Instance.notebooks >= 2 || debug)
        {
            RaycastHit raycastHit;
            if (Physics.Linecast(transform.position, player.position, out raycastHit) && raycastHit.transform.CompareTag("Player") && sprite.activeSelf && !look.lookingBehind)
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
            if(anger >= 0.3f && !angry)
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
            if (forceShowTime > 0 && GameManager.Instance.notebooks >= 2 || debug)
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
            RenderSettings.fog = true;
            RenderSettings.ambientLight = new Color(0.6f, 0.117647059f, 0.2f);
            agent.speed += 30f * Time.deltaTime;
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
    public void DoSometingLocationRelated(Vector3 location)
    {
        if(!angry & agent.isActiveAndEnabled)
        {
            agent.SetDestination(location);
            forceShowTime = 3;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (angry && other.transform == player)
        {
            if (GameManager.Instance.finalMode)
            {
                RenderSettings.ambientLight = Color.red;
            }
            else
            {
                RenderSettings.ambientLight = intial;
            }
            RenderSettings.fog = false;
            Transform playerWarp = WanderPoints.Instance.GetWanderPoint();
            player.position = new Vector3(playerWarp.position.x, player.position.y, playerWarp.position.z);
            gameObject.SetActive(false);
        }
    }
}
