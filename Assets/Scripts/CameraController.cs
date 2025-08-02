using UnityEngine;


public class CameraController : MonoBehaviour
{
    WheelMovement wm;
    Rigidbody2D rb;
    public float rotAccell, minOffset, maxOffset;
    Vector2 anticipation, targetAnticipation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wm = FindFirstObjectByType<WheelMovement>();
        rb = wm.GetComponent<Rigidbody2D>();
        transform.position = rb.position;
        transform.up = wm.GetNormal();
    }

    void Update()
    {
        AnticipateMovement();
        RotateCamera();
        CenterCamera();
    }

    void RotateCamera()
    {
        //transform.up = wm.GetNormal();
        transform.up = Vector2.MoveTowards(transform.up, wm.GetNormal(), rotAccell * Time.deltaTime);
    }
    
    void CenterCamera()
    {
        Vector3 tar;
        if(Mathf.Abs(anticipation.magnitude) >= minOffset)
        {
            Vector2 offset = anticipation.normalized * Mathf.Min(anticipation.magnitude, maxOffset);
            tar = rb.position + offset;
        }
        else tar = rb.position;      
        tar.z = -10;
        FollowTarget(tar);
    }

    void FollowTarget(Vector3 tar)
    {
        float diff = (transform.position - tar).magnitude;
        float followSpeed = Mathf.Sqrt(diff)*2;
        transform.position = Vector3.Lerp(transform.position, tar, followSpeed * Time.deltaTime);
        Physics2D.SyncTransforms();
    }


    void AnticipateMovement()
    {
        targetAnticipation = rb.linearVelocity;
        anticipation = Vector2.Lerp(anticipation, targetAnticipation, Time.deltaTime);       
        Debug.DrawRay(rb.transform.position, anticipation, Color.red);
        
    }
}
