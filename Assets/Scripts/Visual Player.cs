using UnityEngine;

public class VisualPlayer : MonoBehaviour
{
    public WheelMovement _playerMovement;
    
   
    void Awake()
    {
        _playerMovement = FindFirstObjectByType<WheelMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = _playerMovement.GetNormal();
    }
}
