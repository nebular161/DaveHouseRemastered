using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TopSecretElevatorEntrance : MonoBehaviour
{
    public Material secret;

    private void Update()
    {
        if (ItemManager.Instance.items[ItemManager.Instance.selectedItem] == 3)
        {
            gameObject.GetComponent<MeshRenderer>().material = secret;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(ItemManager.Instance.items[ItemManager.Instance.selectedItem] == 3)
        {
            if(other.tag == "Player")
            {
                ItemManager.Instance.ReplaceItem(ItemManager.Instance.selectedItem, 0);
                StartCoroutine(ChangeElevatorPosition());
            }
        }
    }
    IEnumerator ChangeElevatorPosition()
    {
        transform.DOMoveY(10, 5);
        yield return new WaitForSeconds(Random.Range(30f, 70f));
        transform.DOMoveY(0, 5);
    }
}
