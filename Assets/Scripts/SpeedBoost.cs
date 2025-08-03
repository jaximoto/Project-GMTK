using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] public float boostInc;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log($"Collided with game object {collider.gameObject.name}");
        if (collider.gameObject.name == "Wheel")
        {
            Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
            rb.linearVelocity *= boostInc;
        }
    }
}
