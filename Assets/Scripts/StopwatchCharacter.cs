using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopwatchCharacter : MonoBehaviour
{
    NavMeshAgent agent;
    Transform spawnPoint;
    public Transform player;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spawnPoint = WanderPoints.Instance.GetWanderPoint();
        transform.position = spawnPoint.position;
    }
    void Update()
    {
        agent.SetDestination(player.position);
    }
}
