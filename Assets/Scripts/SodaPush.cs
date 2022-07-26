using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SodaPush : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 thingy;
    bool caughtInSoda;
    float failSave;

    // mystman wrote this iirc
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(caughtInSoda)
        {
            agent.velocity = thingy;
        }
        if(failSave > 0)
        {
            failSave -= Time.deltaTime;
        }
        else
        {
            caughtInSoda = false;      
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Soda"))
        {
            caughtInSoda = true;
            thingy = other.GetComponent<Rigidbody>().velocity;
            failSave = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        caughtInSoda = false;
    }
}
