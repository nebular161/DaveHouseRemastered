using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dave : MonoBehaviour
{
    public bool playerSeen;

    public Transform player;
    NavMeshAgent agent;

    public AudioSource daveAud;

    public float coolDown;

    public float currentSpeed, normalSpeed, fastSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetPlayer();
    }
    private void FixedUpdate()
    {
        Vector3 dir = player.position - transform.position;
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, dir, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform == player)
        {
            if (!playerSeen && !daveAud.isPlaying)
            {
                print("Player seen");
            }
            playerSeen = true;
            currentSpeed = fastSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
            if(playerSeen)
            {
                playerSeen = false;
            }
        }
    }
    public void TargetPlayer()
    {
        agent.SetDestination(player.position);
    }
}
