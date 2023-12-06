using UnityEngine;

public class Line : MonoBehaviour
{
    public Vector3 p0, p1;
    public Line()
    {

    }
    public Line(Vector3 p0, Vector3 p1)
    {
        this.p0 = p0;
        this.p1 = p1;
    }
}
