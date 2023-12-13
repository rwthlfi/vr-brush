using UnityEngine;
using System.Collections.Generic;


public class Stroke : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<Vector3> segments;

    private void Awake()
    {
        segments = new List<Vector3>();
        this.lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    public void AddPoint(Vector3 nextPoint)
    {
        segments.Add(nextPoint);
        Vector3[] smoothedPoints = EasyCurvedLine.LineSmoother.SmoothLine(segments.ToArray(), 0.15f);
        lineRenderer.positionCount = smoothedPoints.Length;
        lineRenderer.SetPositions(smoothedPoints);
    }

    //clean up stuff thats no longer needed
    public void Finish()
    {
        if(segments.Count == 1)
        {
            Destroy(gameObject);
        }
        segments.Clear();
        Destroy(this);
    }
}
