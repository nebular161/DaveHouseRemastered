using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Elevator : MonoBehaviour
{
    int notebooksUponTouch;
    public MeshCollider thing;
    private void OnTriggerEnter(Collider other)
    {
        if(transform.position.y == 10)
        {
            if(other.tag == "Player")
            {
                transform.DOMoveY(120, 30);
            }
        }
        else if(transform.position.y == 120)
        {
            if(other.tag == "Player")
            {
                transform.DOMoveY(10, 30);
                thing.enabled = false;
            }
        }
    }
}
