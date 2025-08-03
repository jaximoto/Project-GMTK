using UnityEngine;

public class Cone : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] public float torque = 100f;
    [SerializeField] public float force = 100f;

    [SerializeField] public float gravity;
    [SerializeField] public float termVel;
    private float fallSpeed;

    public Transform core;

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
        SoundManager.PlayEFXRandomSoundPitch(SoundType.CONE);
        // Apply upward force
        Vector2 up = GetNormal();
        Debug.Log(up);
        Debug.DrawRay(transform.position, up, Color.blue);
        rb.AddForce(up * force, ForceMode2D.Impulse);

        // Apply random torque
        float randomTorque = Random.Range(-1f, 1f) * torque;

        rb.AddTorque(randomTorque, ForceMode2D.Impulse);
        
    }


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


    //void ApplyGravity()
    //{
    //    Vector2 fallVel;
    //    Vector2 down = -GetNormal();
    //    fallSpeed = Mathf.MoveTowards(fallSpeed, termVel, gravity * Time.deltaTime);
    //    fallVel = fallSpeed * down; 
    //    //Scale based off distance
    //    rb.linearVelocity += fallVel;

    //}

}
