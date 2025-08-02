using UnityEngine;

public class WheelMovement : MonoBehaviour
{

    [SerializeField] public float torqueAmount = 10f;
    [SerializeField] public float rotDecelerationRate = 2.0f;
    [SerializeField] public float linDecelerationRate = 100.0f;
    [SerializeField] public float rotBrakeAmplifier = 50.0f;
    [SerializeField] public float linBrakeAmplifier = 50.0f;
    [SerializeField] public float speedDashInc = 10.0f;
    [SerializeField] public float groondPoondAccel = 5.0f;

    private Rigidbody2D rb;
    private bool applyInput;
    private float speedDashBuildUp;
    private bool speedDashing;
    private Vector2 properAxis;

    public int speedDashUnleashed;
    [SerializeField] public int speedDashLag = 1000;

    public Transform core;
    
    public float gravity, groundedGravity, termVel;
    private float fallSpeed;
    bool grounded;
    bool acceptingInput;
    
    
    //Relativityitiytiytyi
    public Vector2 up, right;
    public Transform axisSprite;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speedDashBuildUp = 0.0f;
        speedDashing = false;
        acceptingInput = true;
    }


    // Update is called once per frame
    void Update()
    {
        applyInput = false;
        UpdateState();
        if (!acceptingInput)
        {
            Debug.Log($"Accepting Input = {acceptingInput}");
        }
        if (acceptingInput)
        {
            HandleMovement(); 
        }
    }


    void FixedUpdate()
    {
        /* Remove force */
        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, Time.deltaTime * 1);

        // do grounds check
        ApplyGravity();
    }


    void UpdateState()
    {
        acceptingInput = !speedDashing;

        if (speedDashing)
        {
            speedDashUnleashed++;
        }

        if (speedDashUnleashed > speedDashLag)
        {
            speedDashing = false;
        }
    }


    void UpdateAxis()
    {
        properAxis = core.position - transform.position;
    }


    void SpeedDashBuildUp(float s)
    {
        speedDashBuildUp += (s * speedDashInc * Time.deltaTime);
        transform.Rotate(0f, 0f, s * speedDashInc * torqueAmount * Time.deltaTime);
        rb.linearVelocity = Vector2.zero;
    }


    void UnleashSpeedDash()
    {
        speedDashing = true;
        rb.angularVelocity = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.AddTorque(speedDashBuildUp*1000, ForceMode2D.Impulse);
        speedDashBuildUp = 0f;
        speedDashUnleashed = 0;
    }


    void Brake()
    {
        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, Time.deltaTime * rotBrakeAmplifier);
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime * linBrakeAmplifier);
    }


    void GroondPoond()
    {
        Vector2 down = -GetNormal();
        fallSpeed = Mathf.MoveTowards(fallSpeed, termVel, gravity * groondPoondAccel * Time.deltaTime);
        Vector2 fallVel = fallSpeed * down; 
        rb.linearVelocity += fallVel;
    }


    void HandleMovement()
    {

        if (!grounded)
        {

            if (Input.GetKey(KeyCode.S))
            {
                GroondPoond();
            }
            else
            {
                return;
            }
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
        else if (Input.GetKeyUp(KeyCode.S))
        {
            UnleashSpeedDash();
            return;
        }

        if (speedDashing)
        {
            return;
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
        Vector2 normal = GetDistance().normalized;
        Debug.DrawRay(transform.position, normal, Color.green);
        return normal; 
    }

    public Vector2 GetDistance()
    {
        return transform.position - core.position;
    }

    float ScaleGravity()
    {
        return Mathf.Abs(GetDistance().magnitude);
    }

    /* Gravity */
    void ApplyGravity()
    {
        Vector2 fallVel;
        Vector2 down = -GetNormal();
        CheckGround(down);
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
        Debug.DrawRay(transform.position, fallVel, Color.yellow);
        //Scale based off distance
        rb.linearVelocity += fallVel;
    }


    void CheckGround(Vector2 down)
    {
        
        Physics2D.queriesStartInColliders = false;
        Debug.DrawRay(transform.position, down * (transform.localScale.x * 1.15f), Color.blue);
        grounded = Physics2D.Raycast(transform.position, down, transform.localScale.x * 1.15f);
        Physics2D.queriesStartInColliders = true;
    }
}
