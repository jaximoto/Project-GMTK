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
    
    public float gravity, groundedGravity, termVel;
    private float fallSpeed;
    bool grounded;

    //Relativityitiytiytyi
    public Vector2 up, right;
    public Transform axisSprite;

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
        
        //UpdateAxis();
        HandleMovement(); 
        
    }


    void FixedUpdate()
    {
        /* Remove force */
        if (!applyInput && !speedDashing)
        {
            rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, Time.deltaTime * rotDecelerationRate);
        }
        // do grounds check
        ApplyGravity();
    }


    void UpdateAxis()
    {
        properAxis = core.position - transform.position;
    }


    void SpeedDashBuildUp(float s)
    {
        speedDashing = true;
        speedDashBuildUp += (s * speedDashInc * Time.deltaTime);
        transform.Rotate(0f, 0f, s * speedDashInc * torqueAmount * Time.deltaTime);
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



    //Decoupling in case we use a different map than a circle
    public Vector2 GetNormal() 
    {
        Vector2 normal = (transform.position - core.position).normalized;
        Debug.DrawRay(transform.position, normal, Color.green);
        return normal; 
    }

    /* Gravity */
    void ApplyGravity()
    {
        Vector2 fallVel;
        Vector2 down = -GetNormal();
        CheckGround(down);
        Debug.Log($"grounded = {grounded}");
        if (grounded)
        {
            fallSpeed = Mathf.MoveTowards(fallSpeed, groundedGravity, gravity * Time.deltaTime);
            fallVel = fallSpeed * down;

        }
        else
        {
            fallSpeed = Mathf.MoveTowards(fallSpeed, termVel, gravity * Time.deltaTime);
            fallVel = fallSpeed * down; 
        }
        rb.linearVelocity += fallVel;
    }


    void CheckGround(Vector2 down)
    {
        Physics2D.queriesStartInColliders = false;
        Debug.DrawRay(transform.position, down * (transform.localScale.x * 1.15f), Color.blue);
        grounded = Physics2D.Raycast(transform.position, down, transform.localScale.x * 1.15f);
    }
}
