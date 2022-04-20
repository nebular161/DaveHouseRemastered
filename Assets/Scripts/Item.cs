using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Transform player;

    public string itemName;
    public int itemValue;

    private void Start()
    {
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
                if(raycastHit.transform.name == itemName)
                {
                    Destroy(raycastHit.transform.gameObject);
                    ItemManager.Instance.AddItem(itemValue);
                    return;
                }
            }
        }
    }
}
