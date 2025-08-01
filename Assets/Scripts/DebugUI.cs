using UnityEngine;
using TMPro;
public class DebugUI : MonoBehaviour
{
    public Transform relAxis;
    public TMP_Text linVelTxt, rpmsTxt, loopsTxt, timeTxt, ringletsTxt;

    int loops, ringlets;
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
        angVel = rb.angularVelocity;
    }
    void DrawUI()
    {
        linVelTxt.text = $"Velocity: {linVel}";
        rpmsTxt.text = $"Angular Velocity: {angVel}";
        


    }

    void RotateCompass()
    {
        relAxis.up = Vector2.up;
    }

}
