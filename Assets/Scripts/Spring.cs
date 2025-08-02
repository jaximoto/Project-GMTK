using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Spring : MonoBehaviour
{
    public float springStrength = 10f;
    Animator SpringController;
    int _springHash;
    Vector2 _wheelNormal;
    private void Awake()
    {
        SpringController = GetComponent<Animator>();
        _springHash = Animator.StringToHash("Spring");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("collided");
            if( collision.TryGetComponent<WheelMovement>(out WheelMovement wheel))
            {
                //Debug.Log("Got a rigidbody");
                SpringController.Play(_springHash);

                _wheelNormal = wheel.GetNormal();
                Rigidbody2D rb = wheel.GetComponent<Rigidbody2D>(); 
                //if (rb.)
                wheel.GetComponent<Rigidbody2D>().AddForce(_wheelNormal * springStrength, ForceMode2D.Impulse);
            }
            
            
        }
    }
}
