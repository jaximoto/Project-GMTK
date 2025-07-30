using UnityEngine;

public class WheelMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       HandleMovement(); 
    }


    void HandleMovement()
    {

        float horiz = Input.GetAxisRaw("Horizontal"); 
        float vert = Input.GetAxisRaw("Vertical"); 

        

    }
}
