using UnityEngine;
[RequireComponent(typeof(WheelMovement))]

public class VisualPlayer : MonoBehaviour
{
    WheelMovement _playerMovement;
    Transform _centerOfPlanet;
   
    void Awake()
    {
        _playerMovement = GetComponent<WheelMovement>();
        _centerOfPlanet = _playerMovement.cent
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
