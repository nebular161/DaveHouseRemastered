using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public List<Image> backgrounds;
    public List<Sprite> itemImages;
    public List<Image> itemSlotImages;
    public List<string> itemNames;

    public int lastOne;
    public int selectedItem;
    public int maxSlots;

    public int[] items;

    public TMP_Text itemText;

    void Start()
    {
        selectedItem = 0;
        backgrounds[0].color = Color.red;
        lastOne = selectedItem;
        UpdateName();
    }
    void Update()
    {
        ScrollThroughItems();

        if(Input.GetMouseButtonDown(1))
        {
            UseItem();
        }
    }
    public void ScrollThroughItems()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0 & selectedItem > -0)
        {
            backgrounds[lastOne].color = Color.white;
            selectedItem--;
            backgrounds[selectedItem].color = Color.red;
            lastOne = selectedItem;
            UpdateName();
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0 & selectedItem < maxSlots)
        {
            backgrounds[lastOne].color = Color.white;
            selectedItem++;
            backgrounds[selectedItem].color = Color.red;
            lastOne = selectedItem;
            UpdateName();
        }
    }
    public void UpdateName()
    {
        itemText.text = itemNames[items[selectedItem]];
    }
    public void AddItem(int item)
    {
        if(items[selectedItem] != 0)
        {
            DropItem(items[selectedItem]);
            ReplaceItem(selectedItem, item);
        }
        else
        {
            items[selectedItem] = item;
            itemSlotImages[selectedItem].sprite = itemImages[item];
        }
        UpdateName();
    }
    public void DropItem(int item)
    {
        Debug.Log("Item dropped");
        // do stuff
    }
    public void ReplaceItem(int slot, int item)
    {
        items[slot] = item;
        itemSlotImages[slot].sprite = itemImages[item];
        UpdateName();
    }
    public void UseItem()
    {

    }
}
