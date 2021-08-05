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

        Settings settings = SaveManager.LoadSettings();

        mouseSens = settings.settingValues[3];
    }

    // Update is called once per frame
    void Update()
    {
        x += -Input.GetAxis("Mouse Y") * mouseSens;
        y += Input.GetAxis("Mouse X") * mouseSens;

        //Clamp camera
        x = Mathf.Clamp(x, -90, 90);

        //Rotate camera to axis
        transform.localRotation = Quaternion.Euler(x, 0, 0);
        player.transform.localRotation = Quaternion.Euler(0, y, 0);

    }
}
