using UnityEngine;

public class WheelMovement : MonoBehaviour
{

    [SerializeField] public float torqueAmount = 10f;
    [SerializeField] public float rotDecelerationRate = 2.0f;
    [SerializeField] public float linDecelerationRate = 100.0f;
    [SerializeField] public float rotBrakeAmplifier = 50.0f;
    [SerializeField] public float linBrakeAmplifier = 50.0f;
    [SerializeField] public float speedDashInc = 10.0f;

    private Rigidbody2D rb;
    private bool applyInput;
    private float speedDashBuildUp;
    private bool speedDashing;
    private Vector2 properAxis;

    public Transform core;
    public float gravity, groundedGravity;
    bool grounded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speedDashBuildUp = 0.0f;
        speedDashing = false;
    }


    // Update is called once per frame
    void Update()
    {
        UpdateAxis();
        HandleMovement(); 
        ApplyGravity();
    }


    void FixedUpdate()
    {
        /* Remove force */
        if (!applyInput && !speedDashing)
        {
            rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, Time.deltaTime * rotDecelerationRate);
        }
    }


    void UpdateAxis()
    {
        properAxis = core.position - transform.position;
    }


    void SpeedDashBuildUp(float s)
    {
        speedDashing = true;
        speedDashBuildUp += (s * speedDashInc * Time.deltaTime);
        transform.Rotate(0f, 0f, speedDashInc * torqueAmount * Time.deltaTime);
        rb.linearVelocity = Vector2.zero;
    }


    void UnleashSpeedDash()
    {
        speedDashing = false;
        rb.AddTorque(speedDashBuildUp*1000, ForceMode2D.Impulse);
        speedDashBuildUp = 0f;
    }


    void Brake()
    {
        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, Time.deltaTime * rotBrakeAmplifier);
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime * linBrakeAmplifier);
    }


    void HandleMovement()
    {
        applyInput = false;

        if (!grounded)
        {
            return;
        }

        /* Breaking and speed dashing */
        if (Input.GetKey(KeyCode.S))
        {
            applyInput = true;

            Brake();

            if (Input.GetKey(KeyCode.A))
            {
                SpeedDashBuildUp(1.0f);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                SpeedDashBuildUp(-1.0f);
            }

        }
        else if (speedDashing)
        {
            Debug.Log(speedDashBuildUp);
            UnleashSpeedDash();
        }

        /* Standard movement */
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(torqueAmount * Time.deltaTime);
            applyInput = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-torqueAmount * Time.deltaTime);
            applyInput = true;
        }

    }


    /* Gravity */
    public Vector2 AimDownComponent()
    {
        return -(transform.position - core.position).normalized;    
    }


    void ApplyGravity()
    {
        CheckGround();

        float useGravity = grounded ? groundedGravity : gravity;
        Vector2 Grav = AimDownComponent() * useGravity;
        Debug.DrawRay(transform.position, Grav, Color.red);
        rb.AddForce(Grav*Time.deltaTime);           
    }


    void CheckGround()
    {
        Debug.DrawRay(transform.position, properAxis * (transform.localScale.x / 2), Color.blue);
        grounded = Physics2D.Raycast(transform.position, properAxis, transform.localScale.x / 2);
    }
}
