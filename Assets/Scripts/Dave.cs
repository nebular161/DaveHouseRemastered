using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dave : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;

    public AudioSource daveAudio;
    public AudioClip[] lostClips, foundClips, randomClips;
    public AudioClip beenHit;

    float angleDiff;

    public float turnSpeed, currentPriority;
    public float normalSpeed, fastSpeed, currentSpeed;

    float coolDown = 1, spinningTime = 0, piedTime;
    public bool playerSeen, pied, spinning;

    public AudioSource wheelchairAudio;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Wander();
    }
    private void FixedUpdate()
    {
        Vector3 direction = player.position - transform.position;
        RaycastHit raycastHit;
        if(!GameManager.Instance.finalMode)
        {
            if (Physics.Raycast(transform.position, direction, out raycastHit) & raycastHit.transform.CompareTag("Player"))
            {
                if (!playerSeen && !daveAudio.isPlaying)
                {
                    daveAudio.PlayOneShot(foundClips[Random.Range(0, foundClips.Length - 1)]);
                }
                playerSeen = true;
                GoToPlayer();
                currentSpeed = fastSpeed;
                return;
            }
            currentSpeed = normalSpeed;
            if (playerSeen & coolDown <= 0)
            {
                if (!daveAudio.isPlaying)
                {
                    daveAudio.PlayOneShot(lostClips[Random.Range(0, lostClips.Length - 1)]);
                }
                playerSeen = false;
                Wander();
                return;
            }
            if (agent.velocity.magnitude <= 1 & coolDown <= 0 & (transform.position - agent.destination).magnitude < 5)
            {
                Wander();
            }
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        angleDiff = Mathf.DeltaAngle(transform.eulerAngles.y, Mathf.Atan2(agent.steeringTarget.x - transform.position.x, agent.steeringTarget.z - transform.position.z) * 57.29578f);
        if(spinningTime <= 0 && piedTime <= 0)
        {
            if (Mathf.Abs(angleDiff) < 5)
            {
                transform.LookAt(new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z));
                agent.speed = currentSpeed;
            }
            else
            {
                transform.Rotate(new Vector3(0, turnSpeed * Mathf.Sign(angleDiff) * Time.deltaTime, 0));
                agent.speed = 0;
            }
            pied = false;
            spinning = false;
        }
        else if(spinningTime > 0)
        {
            agent.speed = 0;
            transform.Rotate(new Vector3(0, 180 * Time.deltaTime, 0));
            spinningTime -= Time.deltaTime;
            spinning = true;
        }
        else if(piedTime > 0)
        {
            agent.speed = 0;
            piedTime -= Time.deltaTime;
            pied = true;
        }
        wheelchairAudio.pitch = (agent.velocity.magnitude + 1) * Time.timeScale;
    }
    public void Wander()
    {
        Transform wanderPoint = WanderPoints.Instance.GetWanderPoint();
        agent.SetDestination(wanderPoint.position);
        if(!daveAudio.isPlaying & coolDown <= 0)
        {
            daveAudio.PlayOneShot(randomClips[Random.Range(0, randomClips.Length - 1)]);
        }
        coolDown = 1;
        currentPriority = 0;
    }
    public void GoToPlayer()
    {
        agent.SetDestination(player.position);
        coolDown = 0.5f;
    }
    public void StartSpinning()
    {
        spinningTime = 15;
    }
    public void Hear(Vector3 soundLocation, float priority)
    {
        if(priority >= currentPriority)
        {
            agent.SetDestination(soundLocation);
            currentPriority = priority;
        }
    }
    public void GetHitByPie()
    {
        piedTime = 15;
        if (daveAudio.isPlaying)
        {
            daveAudio.Stop();
        }
        daveAudio.clip = beenHit;
        daveAudio.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pie"))
        {
            GetHitByPie();
            Destroy(other.gameObject);
        }
    }
}
