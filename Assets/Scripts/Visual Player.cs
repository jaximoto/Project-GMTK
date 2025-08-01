using UnityEngine;

public class VisualPlayer : MonoBehaviour
{
    public WheelMovement _playerMovement;
    public Rigidbody2D _playerRigidbody;
    Vector2 _tmp;
    Vector3 _localScale;
    Vector3 _flippedLocalScale;
    void Awake()
    {
        _playerMovement = FindFirstObjectByType<WheelMovement>();
        _localScale = transform.localScale;
        _flippedLocalScale = new Vector3(transform.localScale.x * -1, _localScale.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = _playerMovement.GetNormal();
        
        if (_playerRigidbody.angularVelocity > 0)
        {
            //Debug.Log("In if");
            /*
            _tmp = Vector2.Perpendicular(_playerMovement.GetNormal());
            transform.right = new Vector2(-_tmp.x, _tmp.y);
            */
            transform.localScale = _flippedLocalScale;

        }

        else
        {
            //transform.right = -Vector2.Perpendicular(_playerMovement.GetNormal());
            transform.localScale = _localScale;
        }
        
    }
}
