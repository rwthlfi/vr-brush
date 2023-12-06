using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * uses an object as a brush to paint a stroke
 */
public class Brush : MonoBehaviour
{
    private bool _currentlyDrawing = false;
    public GameObject brushShape;
    public Stroke _currentStroke;

    // Start is called before the first frame update
    void Start()
    {
        brushShape = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }

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
            if (nextSegmentReady())
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
        _currentlyDrawing = true;
        _currentStroke = new Stroke(transform.position, brushShape);
    }

    /**
     * Returns wether or not the next segment of the stroke should be drawn
     */
    private bool nextSegmentReady()
    {
        return true;//Time.deltaTime >= 1;
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
