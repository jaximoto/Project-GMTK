using UnityEngine;
using TMPro;
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

    
    public void SetStats()
    {
        linVel = Mathf.Abs(rb.linearVelocity.magnitude);
        angVel = Mathf.Abs(rb.angularVelocity);
        ringlets = _score._ringCount;
        totalScore = _score._score;
    }
    void DrawUI()
    {
        linVelTxt.text = $"Velocity: {linVel}";
        rpmsTxt.text = $"Angular Velocity: {angVel}";
        ringletsCountTxt.text = $"Ringlets Count: {ringlets}";
        totalScoreTxt.text = $"Total Score: {totalScore}";
    }

    void RotateCompass()
    {
        relAxis.up = Vector2.up;
    }

}
