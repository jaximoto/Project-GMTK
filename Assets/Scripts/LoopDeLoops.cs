using UnityEngine;

public class LoopDeLoops : MonoBehaviour
{
    public Collider2D frontColl, backCollider;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player")
        {
            OpenLoop();
        }
    }


    void OpenLoop()
    {
        backCollider.isTrigger = false;
        frontColl.isTrigger = true;
        Debug.Log("Open");
    }
    public void CloseLoop()
    {
        frontColl.isTrigger = false;
        backCollider.isTrigger = true;
        Debug.Log("Close");
    }

    //if its possible to not close the loop, then we'll have to check and close it ourselves
    void checkOpen()
    {

    }
}
