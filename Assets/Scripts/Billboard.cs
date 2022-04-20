using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{

    void Start()
    {
        cam = Camera.main.transform;
        freeRotation.y = 1;
    }

    static Transform cam;
    Vector3 freeRotation;
    Vector3 eangles = Vector3.zero;


    void LateUpdate()
    {
        transform.LookAt(cam);
        transform.Rotate(0, 180, 0);
        eangles = transform.eulerAngles;
        eangles.x *= freeRotation.x;
        eangles.y *= freeRotation.y;
        eangles.z *= freeRotation.z;
        transform.eulerAngles = eangles;
    }
}