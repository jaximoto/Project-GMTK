using UnityEngine;


public class CameraController : MonoBehaviour
{
    WheelMovement wm;
    Rigidbody2D rb;
    public float camAccell, rotAccell, minOffset,maxOffset;
    bool anticipating;
    Vector2 anticipation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wm = FindFirstObjectByType<WheelMovement>();
        rb = wm.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AnticipateMovement();
        RotateCamera();
        CenterCamera();
    }

    void RotateCamera()
    {
        Debug.Log($"Transform.up is {transform.up}");
        //transform.up = wm.GetNormal();
        transform.up = Vector2.MoveTowards(transform.up, wm.GetNormal(), rotAccell * Time.deltaTime);
    }
    
    void CenterCamera()
    {
        Vector3 tar;
        //float followSpeed;
        if(Mathf.Abs(anticipation.magnitude) >= minOffset)
        {
            anticipating = true;
            Vector2 offset = anticipation.normalized * Mathf.Min(anticipation.magnitude, maxOffset);
            tar = rb.position + offset;
            //followSpeed = camAccell;   
        }
        else 
        { 
            anticipating = false;
            tar = rb.position;
            //followSpeed = camAccell;        
        }
        tar.z = -10;
        FollowTarget(tar, rb.linearVelocity.magnitude);
    }

    //new Camera follower functions
    //maybe remove follow speed/ replace with lin velocity
    void FollowTarget(Vector3 tar, float followSpeed)
    {
        Debug.Log($"tar is {tar}, anticipating? {anticipating}");
        transform.position = Vector3.Lerp(transform.position, tar, followSpeed * Time.deltaTime);
    }

    void AnticipateMovement()
    {
        anticipation = rb.linearVelocity;
        Debug.DrawRay(rb.transform.position, anticipation, Color.red);
        
    }
}
