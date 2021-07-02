using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public Transform player;

    public float mouseSens;

    float x = 0;
    float y = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        y += Input.GetAxisRaw("Mouse X") * mouseSens;
        x += -Input.GetAxisRaw("Mouse Y") * mouseSens;

        x = Mathf.Clamp(x, -90, 90);

        transform.localRotation = Quaternion.Euler(x, 0, 0);

        player.transform.localRotation = Quaternion.Euler(0, y, 0);

    }
}
