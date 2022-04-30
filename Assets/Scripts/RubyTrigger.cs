using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyTrigger : MonoBehaviour
{
    public RubyMan rubyMan;
    public Transform go, flee;

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            rubyMan.DoSometingLocationRelated(go.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            rubyMan.DoSometingLocationRelated(flee.position);
        }
    }
}
