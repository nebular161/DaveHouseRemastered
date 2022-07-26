using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Move : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    public float sprintModifier;

    public LayerMask ground;
    public Transform groundDetector;

    public float stamina, staminaRate, maxStamina, jumpForce;

    public bool isWalking, lockPos, enableJumping;

    public Slider staminaBar;

    public float rigidbodyVelocity;

    public float minVelocity, maxVelocity;

    public static Vector3 transPos;

    private float baseFOV;
    public float sprintFOVModifier;

    void Start()
    {
        baseFOV = Camera.main.fieldOfView;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        //Boolean
        bool jump = Input.GetKeyDown(KeyCode.Space);

        //States
        bool isGrounded = Physics.CheckSphere(groundDetector.position, 0.4f, ground);
        bool isJumping = jump && isGrounded;

        //Joumping
        if (isJumping && enableJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (rigidbodyVelocity > minVelocity && rigidbodyVelocity < maxVelocity)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        rb.isKinematic = lockPos;
    }
    private void FixedUpdate()
    {
        if (!lockPos)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            //Booleans
            bool sprint = Input.GetKey(KeyCode.LeftShift) & stamina >= 0f || Input.GetKey(KeyCode.RightShift) & stamina >= 0f;

            //states
            bool isSprinting = sprint && y > 0;

            //Movement
            Vector3 direction = new Vector3(x, 0, y).normalized;
            direction.Normalize();

            float adjustedSpeed = speed;
            if (isSprinting)
            {
                adjustedSpeed *= sprintModifier;
            }
            else
            {
                adjustedSpeed = speed;
            }

            Vector3 targetVelocity = transform.TransformDirection(direction) * adjustedSpeed * Time.deltaTime;
            targetVelocity.y = rb.velocity.y;
            rb.velocity = targetVelocity;
            //fov junk
            if(isSprinting)
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f);
            }
            else
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseFOV, Time.deltaTime * 8f);
            }
            //Stamina bar (referenced from baldi)
            if (isSprinting)
            {
                if (stamina > 0f)
                {
                    stamina -= staminaRate * Time.deltaTime;
                }
                if (stamina < 0f & stamina > -5f)
                {
                    stamina = -5f;
                }
            }
            else if (stamina < maxStamina && !isWalking)
            {
                stamina += staminaRate * Time.deltaTime;
            }
            staminaBar.value = stamina / maxStamina * 100f;
            rigidbodyVelocity = rb.velocity.magnitude;
        }

        
    }
}
