using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DaveSprites : MonoBehaviour
{
    public int angle;
    public float angleF;

    public float angleOffset;

    SpriteRenderer sprite;

    Transform cam;
    public Transform body;

    public Sprite[] sprites = new Sprite[16];
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        cam = Camera.main.transform;
    }
    void Update()
    {
        angleF = (Mathf.Atan2(cam.position.z - body.position.z, cam.position.x - body.position.x) * 57.29578f) + angleOffset;
        if(angleF < 0)
        {
            angleF += 360;
        }
        angleF += body.eulerAngles.y;
        angle = Mathf.RoundToInt(angleF / 22.5f);
        while(angle < 0 || angle >= 16)
        {
            angle += (int)(-16 * Mathf.Sign(angle));
        }
        sprite.sprite = sprites[angle];
    }
}
