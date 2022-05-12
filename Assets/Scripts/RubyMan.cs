using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RubyMan : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    [SerializeField] float anger;
    public SpriteRenderer spriteRenderer;

    public bool gettingAngry, angry;

    AudioSource audioSource;

    public AudioClip intro, loop;
    public Sprite angryRuby, normalRuby;

    Color intial;
    void Start()
    {
        intial = RenderSettings.ambientLight;
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        Wander();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit) && raycastHit.transform == transform)
        {
            gettingAngry = true;
        }
        else
        {
            gettingAngry = false;
        }
    }
    void Update()
    {
        if(Vector3.Distance(transform.position, agent.destination) < 5 && !angry)
        {
            Wander();
        }
        if(gettingAngry)
        {
            anger += Time.deltaTime;
            if(anger >= 0.45f && !angry)
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
        if(angry)
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
    public void Wander()
    {
        Transform wanderPoint = WanderPoints.Instance.GetWanderPoint();
        agent.SetDestination(wanderPoint.position);
    }
    private void OnTriggerStay(Collider other)
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
            GameManager.Instance.UnlockTrophy(162188);
            angry = false;
            anger = 0;
            audioSource.Stop();
            spriteRenderer.sprite = normalRuby;
            Wander();
        }
    }
}
