using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    AudioSource audioSource;

    public Transform player;
    public BoxCollider doorCollider, doorTrigger;

    public bool doorLocked;
    public bool doorOpen;
    public bool lockInfinite;
    public bool nineDoor, blueLock, redLock, normalDoor;
    public float openingDistance;
    public float lockTime;
    public float openTime;

    public MeshRenderer inside, outside;

    public Material openMaterial, closedMaterial, lockedMaterial, closedNormal;

    public AudioClip doorOpenAud, doorCloseAud, doorUnlock;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(!nineDoor && !blueLock && !redLock)
        {
            normalDoor = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!lockInfinite)
        {
            if (lockTime > 0)
            {
                lockTime -= 1 * Time.deltaTime;
            }
            else if (doorLocked)
            {
                UnlockDoor();
            }
        }

        if(openTime > 0)
        {
            openTime -= 1 * Time.deltaTime;
        }
        if(openTime <= 0 & doorOpen)
        {
            CloseDoor();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit) && ((raycastHit.collider == doorCollider || raycastHit.collider == doorTrigger) & Vector3.Distance(player.position, transform.position) < openingDistance & !doorLocked & !doorOpen))
            {
                if(nineDoor && GameManager.Instance.notebooks >= GameManager.Instance.maxNotebooks - 1)
                {
                    OpenDoor();
                }
                else if(blueLock && ItemManager.Instance.items[ItemManager.Instance.selectedItem] == 7)
                {
                    blueLock = false;
                    audioSource.PlayOneShot(doorUnlock);
                    closedMaterial = closedNormal;
                    OpenDoor();
                    ItemManager.Instance.ReplaceItem(ItemManager.Instance.selectedItem, 0);
                }
                else if(redLock && ItemManager.Instance.items[ItemManager.Instance.selectedItem] == 8)
                {
                    redLock = false;
                    audioSource.PlayOneShot(doorUnlock);
                    closedMaterial = closedNormal;
                    OpenDoor();
                    ItemManager.Instance.ReplaceItem(ItemManager.Instance.selectedItem, 0);
                }
                else if(!nineDoor && !blueLock && !redLock)
                {
                    OpenDoor();
                }
            }
        }
    }
    public void OpenDoor()
    {
        audioSource.clip = doorOpenAud;
        audioSource.Play();
        doorCollider.enabled = false;
        doorOpen = true;
        inside.material = openMaterial;
        outside.material = openMaterial;
        openTime = 3;
    }
    public void LockDoor(float time)
    {
        doorLocked = true;
        lockTime = time;
        inside.material = lockedMaterial;
        outside.material = lockedMaterial;
    }
    public void UnlockDoor()
    {
        doorLocked = false;
        inside.material = closedMaterial;
        outside.material = closedMaterial;
    }
    public void CloseDoor()
    {
        doorCollider.enabled = true;
        doorOpen = false;
        inside.material = closedMaterial;
        outside.material = closedMaterial;
        audioSource.clip = doorCloseAud;
        audioSource.Play();
    }
    public void LockDoorInfinite()
    {
        doorLocked = true;
        lockInfinite = true;
        inside.material = lockedMaterial;
        outside.material = lockedMaterial;
    }
}
