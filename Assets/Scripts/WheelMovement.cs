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
        HandleMovement(); 
    }


    void FixedUpdate()
    {
        /* Remove force */
        if (!applyInput && !speedDashing)
        {
            rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, Time.deltaTime * rotDecelerationRate);
        }
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
        //rb.angularVelocity += speedDashBuildUp;
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

}
