using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] public WheelMovement wheel;
    [SerializeField] public int incAmt;
    [SerializeField] public Checkpoint otherCheckpoint;

    [SerializeField] public bool active;
    [SerializeField] public bool finishLine;
    [SerializeField] public Score score;

    public SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (!finishLine)
        {
            sprite.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!active)
        {
            return;
        }

        if (collider.gameObject.name=="Wheel")
        {
            /* Deactivate me */
            active = false;
            sprite.color = Color.red;

            /* Activate other */
            otherCheckpoint.active = true;
            otherCheckpoint.sprite.color = Color.green;

            /* If finish line checkpoint, increment player torque and add 1 to score */
            if (finishLine)
            {
                IncTorque();
                AddScore();
                Debug.Log(score._score);
            }
        }
    }


    void IncTorque()
    {
        wheel.torqueAmount += incAmt;
    }


    void AddScore()
    {
        score._score += 1;
    }
}
