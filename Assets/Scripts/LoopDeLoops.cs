using UnityEngine;

public class LoopDeLoops : MonoBehaviour
{
    public Collider2D frontColl, backColl;

    public bool frontOpen, backOpen;


    void FrontLoop()
    {
        Debug.Log("opening");
        backOpen = true;
        backColl.isTrigger = true;
        frontOpen = false;
        frontColl.isTrigger = false;
    }
    void MiddleLoop()
    {
        backOpen = false;
        backColl.isTrigger = false;
        frontOpen = true;
        frontColl.isTrigger = true;
    }
    void BackLoop()
    {
        backOpen = true;
        backColl.isTrigger = true;
        frontOpen = false;
        frontColl.isTrigger = false;
    }

    public void GetTrigger(int i)
    {
        checkTrigger(i); 
    }

    void checkTrigger(int i)
    {
        Debug.Log($"i is {i}");
        switch(i)
        {
            case 0:
                if(!backOpen) FrontLoop(); 
                break;
            case 1:
                if(!frontOpen)MiddleLoop();
                break;
            case 2:
                BackLoop();
                break;
        }


    }
}
