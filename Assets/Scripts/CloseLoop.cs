using UnityEngine;

public class CloseLoop : MonoBehaviour
{
    LoopDeLoops ldl;
    void Start()
    {
        ldl = transform.parent.GetComponent<LoopDeLoops>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player")
        {
            ldl.CloseLoop();
        }
    }
}
