using UnityEngine;


public class CameraController : MonoBehaviour
{
    WheelMovement wm;
    Rigidbody2D rb;
    public float camAccell;
    Vector2 anticipation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wm = FindFirstObjectByType<WheelMovement>();
        rb = wm.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
        CenterCamera();
    }

    void RotateCamera()
    {
        transform.up = wm.GetNormal();
    }
    
    void CenterCamera()
    {
        transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, transform.position.z);
    }

    void AnticipateMovement()
    {
        anticipation = rb.linearVelocity;
        Debug.DrawRay(rb.transform.position, anticipation, Color.red);
    }
}
