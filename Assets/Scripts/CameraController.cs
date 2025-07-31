using UnityEngine;


public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float camAccell;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
        CenterCamera();
    }

    void RotateCamera()
    {
        transform.up = -player.GetComponent<WheelMovement>().AimDownComponent();
    }
    
    void CenterCamera()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
    
}
