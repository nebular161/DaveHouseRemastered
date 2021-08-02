using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("NPC"))
        {
            Door temp = GetComponentInParent<Door>();
            if(!temp.doorLocked && !temp.doorOpen)
            {
                temp.OpenDoor();
            }
        }
    }
}
