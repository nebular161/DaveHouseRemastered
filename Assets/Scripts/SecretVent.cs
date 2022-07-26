using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretVent : MonoBehaviour
{
    MeshCollider meshCollider;
    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
    }
    void Update()
    {
        if(ItemManager.Instance.items[ItemManager.Instance.selectedItem] == 9)
        {
            meshCollider.enabled = false;
        }
        else
        {
            meshCollider.enabled = true;
        }
    }
}
