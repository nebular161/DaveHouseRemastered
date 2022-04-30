using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BayerMan : MonoBehaviour
{
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if((transform.position - agent.destination).magnitude < 5)
        {
            Wander();
        }
    }
    public void Wander()
    {
        Transform wanderPoint = WanderPoints.Instance.GetWanderPoint();
        agent.SetDestination(wanderPoint.position);
    }
}
