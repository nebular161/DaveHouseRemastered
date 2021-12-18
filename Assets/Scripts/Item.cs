using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    ItemManager itmManager;
    Transform player;

    public string itemName;

    private void Start()
    {
        itmManager = GameObject.Find("GameManager").GetComponent<ItemManager>();
        player = GameObject.Find("Player").transform;
        gameObject.name = itemName;
    }
    void Update()
    {
        RaycastHit raycastHit;
        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0 && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit))
        {
            if(Vector3.Distance(player.position, transform.position) < 10)
            {
                if(raycastHit.transform.name == "Itm_Cake")
                {
                    Destroy(raycastHit.transform.gameObject);
                    itmManager.AddItem(1);
                    return;
                }
                else if (raycastHit.transform.name == "Itm_Pie")
                {
                    Destroy(raycastHit.transform.gameObject);
                    itmManager.AddItem(2);
                    return;
                }
            }
        }
    }
}
