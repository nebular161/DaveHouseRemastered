using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public int item;
    public Collider player;
    public bool mysteryItem;
    private void OnTriggerEnter(Collider other)
    {
        if(other == player)
        {
            if(ItemManager.Instance.items[ItemManager.Instance.selectedItem] == 4)
            {
                if (mysteryItem)
                {
                    item = Random.Range(1, ItemManager.Instance.itemDrop.Length);
                }
                ItemManager.Instance.ReplaceItem(ItemManager.Instance.selectedItem, item);
            }
        }
    }
}
