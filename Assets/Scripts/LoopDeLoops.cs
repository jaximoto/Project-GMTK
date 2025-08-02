using UnityEngine;

public class LoopDeLoops : MonoBehaviour
{
    public Collider2D frontColl, backColl;

    bool frontOpen, backOpen, looping;
    bool dir;

    void FrontLoop()
    {
        Debug.Log("opening");
        if (dir)
        {
            backColl.isTrigger = true;
            frontColl.isTrigger = false;
        }
        else
        {
            backColl.isTrigger = false;
            looping = false;
        }
    }

    //we want this to switch based off direction
    void MiddleLoop(bool dir)
    {
        backColl.isTrigger = !dir;
        frontColl.isTrigger = dir;
    }
    void BackLoop()
    {
        if (dir)
        {
            frontColl.isTrigger = false;
            looping = false;
        }
        else
        {
            backColl.isTrigger = false;
            frontColl.isTrigger = true;
        }
    }

    public void GetTrigger(int i)
    {
        CheckTrigger(i); 
    }

    //check direction of loop
    void SetLoopState(int i)
    {
        if (!looping)
        {
            if (i == 0) dir = true;
            else if (i == 2) dir = false;
        }
    }
    void CheckTrigger(int i)
    {
        Debug.Log($"i is {i}");
        switch(i)
        {
            case 0:
                if (!looping) SetLoopState(i); 
                FrontLoop(); 
                break;
            case 1:
                MiddleLoop(dir);
                break;
            case 2:
                if(!looping) SetLoopState(i);
                BackLoop();
                break;
        }


    }
}
