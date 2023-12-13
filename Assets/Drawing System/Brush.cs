using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/**
 * uses an object as a brush to paint a stroke
 */
public class Brush : MonoBehaviour
{
    private bool _currentlyDrawing = false;
    public Stroke _currentStroke;

    // Update is called once per frame
    void Update()
    {
        if (_currentlyDrawing)
        {
            if (!Input.GetKey("mouse 0"))
            {
                finishStroke();
                return;
            }
            if (nextSegmentShouldDraw())
            {
                drawNextSegment();
                return;
            }
        }
        else if (Input.GetKeyDown("mouse 0"))
        {
            initiateStroke();
            return;
        }
    }

    /**
     * Start drawing by creating a new Stroke and giving it the beginning of the stroke
     */
    private void initiateStroke()
    {
        GameObject newStroke = new GameObject();
        _currentStroke = newStroke.AddComponent<Stroke>();

        Material material = Resources.Load<Material>("Assets/Drawing System/BrushMaterials/Default-Stroke.mat");
        
        _currentStroke.lineRenderer.material = material;
        _currentStroke.lineRenderer.material.color = Color.red;
        _currentStroke.lineRenderer.startWidth = 1;
        _currentStroke.lineRenderer.endWidth = 1;
        _currentStroke.lineRenderer.sortingOrder = 1;
        _currentStroke.lineRenderer.numCornerVertices = 0;
        _currentStroke.lineRenderer.numCapVertices = 4;

        _currentStroke.AddPoint(this.transform.position);

        _currentlyDrawing = true;
    }

    /**
     * Returns wether or not the next segment of the stroke should be drawn
     */
    private bool nextSegmentShouldDraw()
    {

        return Vector3.Distance(_currentStroke.segments.Last(), transform.position) > 1.0f;
    }

    /**
     * Create a new segment and give it to the stroke
     */
    private void drawNextSegment()
    {
        _currentStroke.AddPoint(this.transform.position);
    }

    /**
     * Finish the Stroke by applying the ending of the stroke
     */
    private void finishStroke()
    {
        _currentlyDrawing = false;
    }
}
