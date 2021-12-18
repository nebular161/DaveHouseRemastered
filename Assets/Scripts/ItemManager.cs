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

    public Move playerMovement;

    public Transform player, cam;
    public GameObject pieMovingThing;

    public GameObject[] itemDrop;

    Vector3 playerPosition;

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

        if(Input.GetMouseButtonDown(1) && items[selectedItem] != 0 & Time.timeScale != 0)
        {
            UseItem();
        }
        if(Input.GetKeyDown(KeyCode.R) && items[selectedItem] != 0 & Time.timeScale != 0)
        {
            Drop();
        }

        playerPosition = player.position;
        playerPosition.y = player.position.y + 3;
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
            Drop();
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
        Instantiate(itemDrop[item], playerPosition, player.rotation);
        ReplaceItem(selectedItem, 0);
    }
    public void Drop()
    {
        DropItem(items[selectedItem]);
        UpdateName();
    }
    public void ReplaceItem(int slot, int item)
    {
        items[slot] = item;
        itemSlotImages[slot].sprite = itemImages[item];
        UpdateName();
    }
    public void UseItem()
    {
        switch (items[selectedItem])
        {
            case 1:
                playerMovement.stamina = 250;
                ReplaceItem(selectedItem, 0);
                break;
            case 2:
                Instantiate(pieMovingThing, playerPosition, cam.rotation);
                ReplaceItem(selectedItem, 0);
                break;
            case 3:
                Debug.Log("haha this item has no use");
                break;
        }
    }
}
