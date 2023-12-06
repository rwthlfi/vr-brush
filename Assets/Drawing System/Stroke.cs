using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Stroke : MonoBehaviour
{

    private List<Vector3> _segments;
    public GameObject brushShape;

    void Start()
    {
        _segments = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint(Vector3 nextPoint)
    {
        _segments.Add(nextPoint);
       Instantiate(brushShape, nextPoint, Quaternion.identity);
    }
}
