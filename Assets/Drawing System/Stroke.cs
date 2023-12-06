using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Stroke : MonoBehaviour
{

    private List<Line> _segments;
    public GameObject brushShape;

    public Stroke(Vector3 startingPoint, GameObject brushShape)
    {
        this.brushShape = brushShape;
        this._segments = new List<Line>();
        _segments.Add(new Line(startingPoint, startingPoint));
        _segments.Last().p1.x += 0.1f; //shift the second point so it doesn't lay on top of the first
    }

    void Start()
    {
        /*this._segments = new List<Line>();
        _segments.Add(new Line(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f)));
        _segments.Last().p1.x += 1.0f; //shift the second point so it doesn't lay on top of the first
        for (int i = 0; i < 10; ++i)
        {
            AddPoint(new Vector3(i* 0.1f, 0.0f, 0.0f));
        }

        foreach (Line line in _segments)
        {
            Instantiate(brushShape, line.p0, Quaternion.identity);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(brushShape, _segments.Last().transform.position, Quaternion.identity) ;
    }

    public void AddPoint(Vector3 nextPoint)
    {
        _segments.Add(new Line(_segments.Last().p1, nextPoint));
        Instantiate(brushShape, nextPoint, Quaternion.identity);
    }
}
