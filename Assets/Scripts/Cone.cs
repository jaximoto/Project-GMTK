using UnityEngine;

public class Cone : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] public float torque = 100f;
    [SerializeField] public float force = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name=="Wheel")
        {
            FlyAway();
        }
    }


    void FlyAway()
    {
        // Apply upward force
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        // Apply random torque
        float randomTorque = Random.Range(-1f, 1f) * torque;

        rb.AddTorque(randomTorque, ForceMode2D.Impulse);
        
    }

}
