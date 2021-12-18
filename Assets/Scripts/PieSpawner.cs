using UnityEngine;

public class PieSpawner : MonoBehaviour
{
    public float speed;
    public float lifeSpan;
    private Rigidbody rb;

    AudioSource source;
    public AudioClip splat;
    void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
    void Update()
    {
        rb.velocity = transform.forward * speed;
        lifeSpan -= Time.deltaTime;
        if(lifeSpan < 0)
        {
            Destroy(gameObject, 0);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.name == "DaveAngry")
        {
            source.clip = splat;
            source.Play();
            Destroy(gameObject, 0);
        }
    }
}
