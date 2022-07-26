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
                if(temp.nineDoor && GameManager.Instance.notebooks >= 9)
                {
                    temp.OpenDoor();
                }
                else if(!temp.nineDoor)
                {
                    temp.OpenDoor();
                }
            }
        }
    }
}
