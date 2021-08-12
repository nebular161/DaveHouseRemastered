using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemManager itmManager;
    public Transform player;
    void Update()
    {
        RaycastHit raycastHit;
        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0 && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit))
        {
            if(Vector3.Distance(player.position, transform.position) < 10)
            {
                if(raycastHit.transform.name == "Itm_Cake")
                {
                    raycastHit.transform.gameObject.SetActive(false);
                    itmManager.AddItem(1);
                    return;
                }
            }
        }
    }
}
