using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    Dave dave;
    public float lifeSpan;
    void Start()
    {
        if(GameObject.Find("DaveAngry").activeInHierarchy)
        {
            dave = GameObject.Find("DaveAngry").GetComponent<Dave>();
            dave.Hear(transform.position, 10);
        }
    }
    private void Update()
    {
        lifeSpan -= Time.deltaTime;
        if(lifeSpan < 0)
        {
            Destroy(gameObject);
        }
    }
}
