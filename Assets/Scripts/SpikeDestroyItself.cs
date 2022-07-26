using UnityEngine;

public class SpikeDestroyItself : MonoBehaviour
{
    float time = 10;
    void Update()
    {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
