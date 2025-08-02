using UnityEngine;


public class CameraController : MonoBehaviour
{
    WheelMovement wm;
    Rigidbody2D rb;
    Transform t;
    public float rotAccell, minOffset, maxOffset;
    Vector2 anticipation, targetAnticipation;
    public GameObject tracking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wm = FindFirstObjectByType<WheelMovement>();
        rb = wm.GetComponent<Rigidbody2D>();
        t = rb.GetComponent<Transform>();
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
        tracking.transform.position = tar;
        tar.z = -10;
        
        FollowTarget(tar);
    }

    void FollowTarget(Vector3 tar)
    {
        float diff = (transform.position - tar).magnitude;
        float followSpeed = Mathf.Sqrt(diff)*20;
        transform.position = Vector3.Lerp(transform.position, tar, followSpeed * Time.deltaTime);
    }

    Vector2 lastPos;
    void AnticipateMovement()
    {   
        Vector2 pos = t.position;
        Vector2 tVel = (pos - lastPos) / Time.deltaTime;
        Debug.Log($"rb.linearVelocity is {rb.linearVelocity} and transVel is {tVel}");

        targetAnticipation = rb.linearVelocity;
        Vector2 newAnticipation = (targetAnticipation + tVel) / 2;  
        anticipation = Vector2.Lerp(anticipation, newAnticipation, Time.deltaTime);       
        Debug.DrawRay(rb.transform.position, anticipation, Color.red);
        lastPos = pos;
    }
}
