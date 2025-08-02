using UnityEngine;

public class LoopTrigger : MonoBehaviour
{
    LoopDeLoops ldl;
    public enum triggerType
    {
        front,
        mid,
        back
    }

    public triggerType type;
    void Start()
    {
        ldl = transform.parent.GetComponent<LoopDeLoops>();     
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"Triggered with col tag = {col.gameObject.tag}");
        if (col.gameObject.tag == "Player")
        {
            ldl.GetTrigger((int) type);        
        }
    }

    
}
