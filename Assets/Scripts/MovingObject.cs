using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed;
    public float lifeSpan;
    private Rigidbody rb;

    [SerializeField] bool randomizeSpeed;
    void Start()
    {
        if(randomizeSpeed)
        {
            speed = Random.Range(40f, 80f);
        }
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
    void Update()
    {
        rb.velocity = transform.forward * speed;
        lifeSpan -= Time.deltaTime;
        if(lifeSpan < 0)
        {
            Destroy(gameObject);
        }
    }
}
