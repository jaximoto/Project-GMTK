using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class Constellation : MonoBehaviour
{
    public GameObject blueStar;

    [ContextMenu("SetSpline")]
    void SetSpline()
    {

        SplineContainer splineCon = GetComponent<SplineContainer>();
        Spline useSpline = splineCon.Spline;
        for (int i = 0; i < useSpline.Count; i++)
        {
            Vector3 starPos = useSpline[i].Position;
            Instantiate(blueStar, starPos + transform.position, Quaternion.identity);
        }
        Transform t = splineCon.gameObject.AddComponent(typeof(Transform)) as Transform;
        DestroyImmediate(splineCon);
    }

}
