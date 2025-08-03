using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Splines;
using Spleen = UnityEngine.Splines.Spline;

public class SplineAssign : MonoBehaviour
{
    SpriteShapeController sc;
    public GameObject newSpline;

    [ContextMenu("SetSpline")]
    void SetSpline()
    {
        sc = GetComponent<SpriteShapeController>();
        SplineContainer splineCon = newSpline.GetComponent<SplineContainer>();
        Spleen useSpline = splineCon.Spline;
        sc.spline.Clear();
        
        for (int i = 0; i < useSpline.Count; i++)
        {
            sc.spline.InsertPointAt(i, useSpline[i].Position);
            sc.spline.SetRightTangent(i, useSpline[i].TangentIn);
            sc.spline.SetLeftTangent(i, useSpline[i].TangentOut);
        }
        
    }

}
