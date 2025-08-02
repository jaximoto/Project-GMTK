using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Spring : MonoBehaviour
{
    public float springStrength = 10f;
    Animator SpringController;
    int _springHash;
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
            if( collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D _rigidbody2D))
            {
                Debug.Log("Got a rigidbody");
                SpringController.Play(_springHash);
                _rigidbody2D.AddForceY(springStrength);
            }
            
            
        }
    }
}
