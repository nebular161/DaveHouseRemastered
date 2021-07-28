using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    AudioSource audioSource;

    public Transform player;
    public BoxCollider doorCollider;

    public bool doorLocked;
    public bool doorOpen;
    public bool lockInfinite;
    public float openingDistance;
    public float lockTime;
    public float openTime;

    public MeshRenderer inside, outside;

    public Material openMaterial, closedMaterial, lockedMaterial;

    public AudioClip openDoor, closeDoor;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

            if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider == doorCollider & Vector3.Distance(player.position, transform.position) < openingDistance & !doorLocked))
            {
                OpenDoor();
            }
        }
    }
    public void OpenDoor()
    {
        audioSource.PlayOneShot(openDoor);
        doorCollider.enabled = false;
        doorOpen = true;
        inside.material = openMaterial;
        outside.material = openMaterial;
        openTime = 3;
    }
    private void OnTriggerStay(Collider other)
    {
        if(!doorLocked & other.CompareTag("NPC"))
        {
            OpenDoor();
        }
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
        audioSource.PlayOneShot(closeDoor);
    }
    public void LockDoorInfinite()
    {
        doorLocked = true;
        lockInfinite = true;
        inside.material = lockedMaterial;
        outside.material = lockedMaterial;
    }
}
