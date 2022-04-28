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
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit) & raycastHit.transform == transform & spriteRenderer.isVisible & sprite.activeSelf)
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
            RenderSettings.ambientLight = new Color(0.6f, 0.117647059f, 0.2f);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            if(GameManager.Instance.finalMode)
            {
                RenderSettings.ambientLight = Color.red;
            }
            else
            {
                RenderSettings.ambientLight = intial;
            }
        }
    }
}
