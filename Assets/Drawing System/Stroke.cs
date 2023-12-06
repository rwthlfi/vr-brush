using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Stroke : MonoBehaviour
{

    public List<Vector3> segments;
    public GameObject brushShape;

    private void Awake()
    {
        segments = new List<Vector3>();
    }

    public void AddPoint(Vector3 nextPoint)
    {

        Vector3 temp = nextPoint - segments.Last();

        float rotationZ = Mathf.Rad2Deg * Mathf.Asin(temp.y / Mathf.Sqrt((temp.x * temp.x) + (temp.y * temp.y)));
        float rotationX = Mathf.Rad2Deg * Mathf.Asin(temp.z / Mathf.Sqrt((temp.y * temp.y) + (temp.z * temp.z)));

        segments.Add(nextPoint);

        Instantiate(brushShape, nextPoint, Quaternion.Euler(rotationX, 0, rotationZ));
    }
    public void FirstPoint(Vector3 nextPoint)
    {
        segments.Add(nextPoint);
    }
}
