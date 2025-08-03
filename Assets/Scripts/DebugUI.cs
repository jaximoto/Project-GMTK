using System;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
public class DebugUI : MonoBehaviour
{
    public Transform relAxis;
    public TMP_Text linVelTxt, rpmsTxt, loopsTxt, timeTxt, ringletsCountTxt, totalScoreTxt;
    public Score _score;

    int loops, ringlets, totalScore;
    float linVel, angVel;

    WheelMovement wm;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wm = FindFirstObjectByType<WheelMovement>();
        rb = wm.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        SetStats();
        DrawUI();
        RotateCompass();
    }

    float RPMs;
    int _loopCount = 0;
    public int comboScore = 100;
    float maxVelocityObtained = 0f;
    public bool comboStarted = false;
    public bool comboEnded = false;
    int comboMultiplier = 0;
    float timer = 0f;
    public void SetStats()
    {
        linVel = Mathf.Abs(rb.linearVelocity.magnitude);
        if (!wm.grounded && !comboStarted)
        {
            //Debug.Log("Started combo!");
            comboStarted = true;
        }
        else if(!wm.grounded && comboStarted)
        {
            timer += Time.deltaTime;   // accumulate the time since last frame

            if (timer >= 1f)           // if 1 second (or more) has passed…
            {
                comboMultiplier++;               // add to your value
                timer = 0f;            // reset the timer
                //Debug.Log(value);
            }
            maxVelocityObtained = Mathf.Max(maxVelocityObtained, linVel);
        }
            
            
    
        else if(wm.grounded && comboStarted)
        {
            
            _score._score += (int) (comboMultiplier * maxVelocityObtained);
            //Debug.Log("Ended combo! Added " + (int)(comboMultiplier * maxVelocityObtained) + " and was ComboMult: " + comboMultiplier + " and MaxVel: " + maxVelocityObtained );
            comboStarted = false;
            comboMultiplier = 0;
            maxVelocityObtained = 0f;
            timer = 0f;
        }
            angVel = Mathf.Abs(rb.angularVelocity);
        RPMs = angVel / (2 * MathF.PI) * 60 / 1000;
        ringlets = _score._ringCount;
        totalScore = _score._score;
        _loopCount = _score._loopCount;
    }
    void DrawUI()
    {
        linVelTxt.text = $"M/S: {linVel}";
        //rpmsTxt.text = $"Angular Velocity: {RPMs}";
        //ringletsCountTxt.text = $"Ringlets Count: {ringlets}";
        totalScoreTxt.text = $"Total Score: {totalScore}";
        loopsTxt.text = $"Laps: {_loopCount}";
    }

    void RotateCompass()
    {
        relAxis.up = Vector2.up;
    }

}
