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
}
