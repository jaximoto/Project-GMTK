using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Transform core;
    public float gravity, groundedGravity;
    bool grounded;
    Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ApplyGravity();
    }


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
        Debug.DrawRay(transform.position, Vector2.down * (transform.localScale.x / 2), Color.blue);
        Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.x / 2);
    }


}
