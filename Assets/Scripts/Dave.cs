using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dave : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public AudioSource daveAudio;
    public AudioClip[] lostClips, foundClips, randomClips;
    public AudioClip beenHit, knife, secret;

    float angleDiff;

    public float turnSpeed, currentPriority;
    public float normalSpeed, fastSpeed, currentSpeed;

    [SerializeField] float spinningTime, piedTime;
    public bool playerSeen, pied, spinning;

    public AudioSource wheelchairAudio;

    [SerializeField] int times = 0;

    private void FixedUpdate()
    {
        Vector3 direction = player.position - transform.position;
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, direction, out raycastHit) & raycastHit.transform.CompareTag("Player"))
        {
            if (!playerSeen && !daveAudio.isPlaying)
            {
                daveAudio.PlayOneShot(foundClips[Random.Range(0, foundClips.Length - 1)]);
            }
            playerSeen = true;
            currentSpeed = fastSpeed;
            return;
        }
        currentSpeed = normalSpeed;
        if (playerSeen)
        {
            if (!daveAudio.isPlaying)
            {
                daveAudio.PlayOneShot(lostClips[Random.Range(0, lostClips.Length - 1)]);
            }
            playerSeen = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
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
            times = 0;
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
    public void GetHitByPie()
    {
        if(!pied)
        {
            piedTime = 15;
        }
        else
        {
            piedTime += 5;
        }
        times++;
        if(times == 3)
        {
            GameManager.Instance.UnlockTrophy(162189);
        }
        if (daveAudio.isPlaying)
        {
            daveAudio.Stop();
        }
        daveAudio.PlayOneShot(beenHit);
        GameManager.Instance.UnlockTrophy(162157);
    }
    public void KnifeAttack()
    {
        if (!spinning)
        {
            spinningTime = 15;
        }
        else
        {
            spinningTime += 5;
        }
        if(daveAudio.isPlaying)
        {
            daveAudio.Stop();
        }
        int secrete = Random.Range(0, 69);
        if (secrete == 44)
        {
            daveAudio.PlayOneShot(secret);
        }
        else
        {
            daveAudio.PlayOneShot(knife);
        }
        GameManager.Instance.UnlockTrophy(162159);
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
