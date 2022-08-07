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
    public GameObject pieMovingThing, sodaObject;

    public GameObject[] itemDrop;

    Vector3 playerPosition;

    public TMP_Text entranceSearcher;

    public Dave dave;
    public AudioSource teleporterAudio;
    public AudioClip bew;

    int timesToTeleport = 10;

    int distanceThingy;

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
            distanceThingy = Mathf.RoundToInt(Vector3.Distance(player.position, secretEntrance.position) / 2.5f);
            entranceSearcher.text = $"DISTANCE_FROM_SECRET: {distanceThingy} ft";
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

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            backgrounds[lastOne].color = Color.white;
            selectedItem = 0;
            backgrounds[selectedItem].color = Color.red;
            lastOne = selectedItem;
            UpdateName();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            backgrounds[lastOne].color = Color.white;
            selectedItem = 1;
            backgrounds[selectedItem].color = Color.red;
            lastOne = selectedItem;
            UpdateName();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            backgrounds[lastOne].color = Color.white;
            selectedItem = 2;
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
        bool hasFoundItemSlot = false;
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i] == 0 && !hasFoundItemSlot)
            {
                items[i] = item;
                itemSlotImages[i].sprite = itemImages[item];
                hasFoundItemSlot = true;
            }
        }
        if (!hasFoundItemSlot)
        {
            Drop();
            ReplaceItem(selectedItem, item);
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
                Instantiate(pieMovingThing, cam.position, cam.rotation);
                ReplaceItem(selectedItem, 0);
                break;
            case 5:
                RaycastHit daveHit;
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out daveHit) && daveHit.transform == dave.transform && Vector3.Distance(dave.transform.position, player.position) <= 15)
                {
                    dave.KnifeAttack();
                    ReplaceItem(selectedItem, 0);
                }
                break;
            case 6:
                timesToTeleport = 10;
                StartCoroutine(TeleportSequence());
                ReplaceItem(selectedItem, 0);
                break;
            case 10:
                Instantiate(sodaObject, cam.position, cam.rotation);
                ReplaceItem(selectedItem, 0);
                break;
            case 11:
                RaycastHit dvdPlayerHit;
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out dvdPlayerHit) && dvdPlayerHit.transform.CompareTag("DVDPlayer") && Vector3.Distance(dvdPlayerHit.transform.position, player.position) <= 15)
                {
                    DiscPlayer.Instance.GetClip();
                    ReplaceItem(selectedItem, 0);
                }
                break;
            default:
                Debug.Log("haha this item has no use");
                break;
        }
    }
    public void TeleportPlayer()
    {
        Transform playerWarp = WanderPoints.Instance.GetWanderPoint();
        player.position = new Vector3(playerWarp.position.x, 2.5f, playerWarp.position.z);
        teleporterAudio.PlayOneShot(bew);
    }
    IEnumerator TeleportSequence()
    {
        if(timesToTeleport >= 0)
        {
            TeleportPlayer();
            timesToTeleport--;
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(TeleportSequence());
        }
    }
}
