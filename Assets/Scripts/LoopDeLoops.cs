using UnityEngine;

public class LoopDeLoops : MonoBehaviour
{
    public Collider2D frontColl;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player")
        {
            OpenLoop();
        }
    }


    void OpenLoop()
    {
        frontColl.isTrigger = true;
    }
    public void CloseLoop()
    {
        frontColl.isTrigger = false;
    }

    //if its possible to not close the loop, then we'll have to check and close it ourselves
    void checkOpen()
    {

    }
}
