using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dave : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;

    float angleDiff;

    public float turnSpeed;
    public float normalSpeed;

    public AudioSource wheelchairAudio;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        angleDiff = Mathf.DeltaAngle(transform.eulerAngles.y, Mathf.Atan2(agent.steeringTarget.x - transform.position.x, agent.steeringTarget.z - transform.position.z) * 57.29578f);

        if(Mathf.Abs(angleDiff) < 5)
        {
            transform.LookAt(new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z));
            agent.speed = normalSpeed;
        }
        else
        {
            transform.Rotate(new Vector3(0, turnSpeed * Mathf.Sign(angleDiff) * Time.deltaTime, 0));
            agent.speed = 0;
        }
        wheelchairAudio.pitch = (agent.velocity.magnitude + 1) * Time.timeScale;
    }
}
