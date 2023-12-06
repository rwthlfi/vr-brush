using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * uses an object as a brush to paint a stroke
 */
public class Brush : MonoBehaviour
{
    private bool currentlyDrawing = false;
    private bool currentStroke;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyDrawing)
        {
            if (!Input.GetKeyDown("Mouse 0"))
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
        else if (Input.GetKeyDown("Mouse 0"))
        {
            initiateStroke();
            return;
        }
    }

    /**
     * Start drawing by creating a new Stroke and giving it the beginning of the stroke
     */
    void initiateStroke()
    {
        currentlyDrawing = true;
        currentStroke = new Stroke(new Mesh());
    }

    /**
     * Returns wether or not the next segment of the stroke should be drawn
     */
    bool nextSegmentReady()
    {
        return true;
    }

    /**
     * Create a new segment and give it to the stroke
     */
    void drawNextSegment()
    {

    }

    /**
     * Finish the Stroke by applying the ending of the stroke
     */
    void finishStroke()
    {
        currentlyDrawing = false;
    } 
}
