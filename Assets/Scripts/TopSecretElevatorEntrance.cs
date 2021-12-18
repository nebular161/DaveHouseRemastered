using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TopSecretElevatorEntrance : MonoBehaviour
{
    public ItemManager itmManager;
    public Material secret;

    private void Update()
    {
        if (itmManager.items[itmManager.selectedItem] == 3)
        {
            gameObject.GetComponent<MeshRenderer>().material = secret;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(itmManager.items[itmManager.selectedItem] == 3)
        {
            if(other.tag == "Player")
            {
                itmManager.ReplaceItem(itmManager.selectedItem, 0);
                transform.DOMoveY(10, 5);
            }
        }
    }
}
