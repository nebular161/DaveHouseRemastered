using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayRobot : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator playRobotSpriteAnimator;

    public float gameCooldown = 15, coolDown;

    public PadGame padGame;
    bool padGameStarted, playerSeen, playerSpotted;

    public PlayerManager playerManager;
    Transform player;

    public AudioSource aud;
    public AudioClip glitch, begin, end;

    public AudioClip[] wanderClips;
    public AudioClip[] foundClips;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = playerManager.transform;
        Wander();
    }
    private void FixedUpdate()
    {
        if(!playerManager.inPadGame)
        {
            Vector3 direction = player.position - transform.position;
            RaycastHit raycastHit;

            if (Physics.Raycast(transform.position, direction, out raycastHit) && raycastHit.transform.CompareTag("Player") && gameCooldown <= 0 && (transform.position - player.position).magnitude <= 80)
            {
                playerSeen = true;
                TargetPlayer();
            }
            else if(playerSeen && coolDown <= 0)
            {
                playerSeen = false;
                Wander();
            }
            else if(agent.velocity.magnitude <= 1 && coolDown <= 0)
            {
                Wander();
            }
            padGameStarted = false;
            return;
        }
        if (!padGameStarted)
        {
            agent.Warp(transform.position - transform.forward * 10);
        }
        padGameStarted = true;
        agent.speed = 0;
        gameCooldown = 20;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 5)
        {
            Wander();
        }
        if(coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        if (gameCooldown >= 0)
        {
            gameCooldown -= Time.deltaTime;
            return;
        }
        playRobotSpriteAnimator.SetBool("virusMode", false);
    }
    public void Wander()
    {
        aud.PlayOneShot(wanderClips[Random.Range(0, wanderClips.Length)]);
        Transform wanderPoint = WanderPoints.Instance.GetWanderPoint();
        agent.SetDestination(wanderPoint.position);
        agent.speed = Random.Range(15f, 17f);
    }
    public void TargetPlayer()
    {
        agent.SetDestination(player.position);
        agent.speed = 20;
        coolDown = 0.2f;
        if (!playerSpotted)
        {
            playerSpotted = true;
            aud.PlayOneShot(foundClips[Random.Range(0, foundClips.Length)]);
        }
    }
    public void StartGame()
    {
        aud.PlayOneShot(begin);
        padGame.gameObject.SetActive(true);
        padGame.problem = 0;
        StartCoroutine(padGame.BeginSequence());
    }
    public void ActivateVirusMode()
    {
        playRobotSpriteAnimator.SetBool("virusMode", true);
        aud.PlayOneShot(glitch);
    }
}
