using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Splines;
using Spleen = UnityEngine.Splines.Spline;

public class SplineAssign : MonoBehaviour
{
    SpriteShapeController sc;
    SpriteShapeRenderer sr;
    EdgeCollider2D ec;
    public SpriteShape ss;
    public PhysicsMaterial2D pm;
    public bool isOpen;

    [ContextMenu("SetSpline")]
    void SetSpline()
    {
        AddComponents();
        
        SplineContainer splineCon = GetComponent<SplineContainer>();
        Spleen useSpline = splineCon.Spline;
        sc.spline.Clear();
        for (int i = 0; i < useSpline.Count; i++)
        {
            sc.spline.InsertPointAt(i, useSpline[i].Position);

            sc.spline.SetRightTangent(i, useSpline[i].TangentIn);
            sc.spline.SetLeftTangent(i, useSpline[i].TangentOut);

            sc.spline.SetTangentMode(i, ShapeTangentMode.Continuous);

            ec.edgeRadius = 0.5f;
        }
        DestroyImmediate(splineCon);
    }
    // + newSpline.transform.position.x    + newSpline.transform.position.y     + newSpline.transform.position.z)
    void AddComponents()
    {
        sr = gameObject.AddComponent(typeof(SpriteShapeRenderer)) as SpriteShapeRenderer;
        sc = gameObject.AddComponent(typeof (SpriteShapeController)) as SpriteShapeController;
        sc.spriteShape = ss;

        sc.spline.isOpenEnded = isOpen;

        ec = gameObject.AddComponent(typeof(EdgeCollider2D)) as EdgeCollider2D;
    }

    
    [ContextMenu("FlipTangents")]
    void FlipTangents()
    {
        SpriteShapeController _sc = GetComponent<SpriteShapeController>();
        for(int i = 0; i < _sc.spline.GetPointCount(); i++)
        {
            Vector3 rTan = _sc.spline.GetLeftTangent(i);
            Vector3 lTan = _sc.spline.GetRightTangent(i);
            _sc.spline.SetRightTangent(i, rTan);
            _sc.spline.SetLeftTangent(i, lTan);
        }
    }
    
    [ContextMenu("FlipHeights")]
    void FlipHeights()
    {
        SpriteShapeController _sc = GetComponent<SpriteShapeController>();
        for (int i = 0; i < _sc.spline.GetPointCount(); i++)
        {
            _sc.spline.SetHeight(i, -_sc.spline.GetHeight(i));
        }
    } 

}
