using UnityEngine;

public class WheelMovement : MonoBehaviour
{

    [SerializeField] public float torqueAmount = 10f;
    [SerializeField] public float rotDecelerationRate = 2.0f;
    [SerializeField] public float linDecelerationRate = 100.0f;
    [SerializeField] public float rotBrakeAmplifier = 50.0f;
    [SerializeField] public float linBrakeAmplifier = 50.0f;
    private Rigidbody2D rb;
    private bool applyInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
       HandleMovement(); 
    }


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            applyInput = true;
            Brake();
        }

        if (!applyInput)
        {
            rb.AddTorque(-rb.angularVelocity * rotDecelerationRate * Time.deltaTime);
            rb.AddForce(-rb.linearVelocity * linDecelerationRate * Time.deltaTime);
        }
    }


    void SpeedDash()
    {
        rb.AddTorque(-rb.angularVelocity * rotDecelerationRate * Time.deltaTime * rotBrakeAmplifier);
        rb.AddForce(-rb.linearVelocity * linDecelerationRate * Time.deltaTime * linBrakeAmplifier);
    }


    void Brake()
    {
        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, Time.deltaTime * rotBrakeAmplifier);
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime * linBrakeAmplifier);
    }


    void HandleMovement()
    {
        applyInput = false;
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
