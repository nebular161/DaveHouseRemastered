using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
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

    public Transform player, cam, secretEntrance;
    public GameObject pieMovingThing;

    public GameObject[] itemDrop;

    Vector3 playerPosition;

    public TMP_Text entranceSearcher;

    public Dave dave;

    private void Awake()
    {
        Instance = this;
    }
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

        if(items[selectedItem] == 3)
        {
            entranceSearcher.gameObject.SetActive(true);
            entranceSearcher.text = $"DISTANCE_FROM_SECRET: {Vector3.Distance(player.position, secretEntrance.position).ToString("0")} m";
        }
        else
        {
            entranceSearcher.gameObject.SetActive(false);
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
                GameManager.Instance.UnlockTrophy(162186);
                ReplaceItem(selectedItem, 0);
                break;
            case 2:
                Instantiate(pieMovingThing, playerPosition, cam.rotation);
                ReplaceItem(selectedItem, 0);
                break;
            case 5:
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit daveHit;
                if(Physics.Raycast(ray, out daveHit) && daveHit.transform == dave.transform && Vector3.Distance(dave.transform.position, player.position) <= 10)
                {
                    dave.KnifeAttack();
                    ReplaceItem(selectedItem, 0);
                }
                break;
            default:
                Debug.Log("haha this item has no use");
                break;
        }
    }
}
