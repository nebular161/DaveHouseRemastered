using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Bombai : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform[] wanderPoints;
    public bool isWandering;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Wander();
    }
    void Update()
    {
        if(!isWandering && !agent.isStopped)
        {
            agent.isStopped = true;
        }
    }
    public void Wander()
    {
        agent.SetDestination(wanderPoints[Random.Range(0, wanderPoints.Length - 1)].position);
        isWandering = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WanderPoint" && isWandering)
        {
            Wander();
        }
    }
}
