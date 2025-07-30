using UnityEngine;

public class WheelMovement : MonoBehaviour
{

    [SerializeField] public float torqueAmount = 10f;
    [SerializeField] public float decelerationRate = 1.0f;
    [SerializeField] public float breakAmplifier = 2.0f;
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
            Break();
            Debug.Log("Breaking");
        }

        if (!applyInput)
        {
            rb.AddTorque(-rb.angularVelocity * decelerationRate * Time.deltaTime);
        }
    }


    void Break()
    {
        //rb.angularVelocity = Mathf.MoveTowards(rb.angularVelocity, 0f, breakAmplifier * Time.deltaTime);
        rb.angularVelocity = 0f;
        rb.linearVelocity = Vector2.zero;
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
