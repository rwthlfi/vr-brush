using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/**
 * uses an object as a brush to paint a stroke
 */
public class Brush : MonoBehaviour
{
    private bool _brushPropertiesChanged = false;
    private bool _currentlyDrawing = false;
    private bool _triggerPressed = false;
    private Stroke _currentStroke;
    public Transform DrawPoint;
    private float _size;
    public float Size
    {
        get
        {
            return _size;
        }
        set
        {
            _size = value;
            _brushPropertiesChanged = true;
        }
    }
    private Color _color;
    public Color Color
    {
        get
        {
            return _color;
        }
        set
        {
            _color = value;
            _brushPropertiesChanged = true;
        }
    }

    private List<UnityEngine.XR.InputDevice> _rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
    private void Awake()
    {
        UnityEngine.XR.InputDeviceCharacteristics desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, _rightHandedControllers);
        _size = 0.2f;
        _color = Color.blue;
        _currentlyDrawing = false;
    }

    public void StartDrawing()
    {
        // _currentlyDrawing = true;
        _triggerPressed = true;
        Debug.Log("Start drawing", gameObject);
        // initiateStroke();
    }

    public void StopDrawing()
    {
        _triggerPressed = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (_currentlyDrawing)
        {
            if (!_triggerPressed)
            {
                finishStroke();
                return;
            }
            if (nextSegmentShouldDraw())
            {
                if (_brushPropertiesChanged)
                {
                    updateBrushProperties();
                }

                drawNextSegment();
                return;
            }
        }
        else if (_triggerPressed)
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

        //This defines how the drawn stroke looks
        _currentStroke.lineRenderer.material = material;
        _currentStroke.lineRenderer.material.color = _color;
        _currentStroke.lineRenderer.startWidth = _size;
        _currentStroke.lineRenderer.endWidth = _size;

        _currentlyDrawing = true;

        drawNextSegment();
    }

    /**
     * Returns wether or not the next segment of the stroke should be drawn
     */
    private bool nextSegmentShouldDraw()
    {
        return Vector3.Distance(_currentStroke.segments.Last(), transform.position) > 0.05f;
    }

    private void updateBrushProperties()
    {
        _currentStroke.lineRenderer.material.color = _color;
        _currentStroke.lineRenderer.startWidth = _size;
        _currentStroke.lineRenderer.endWidth = _size;
    }

    /**
     * Create a new segment and give it to the stroke
     */
    private void drawNextSegment()
    {
        // Position der Pinselspitze: DrawPoint
        _currentStroke.AddPoint(DrawPoint.position);
    }

    /**
     * Finish the Stroke by applying the ending of the stroke
     */
    private void finishStroke()
    {
        _currentStroke.Finish();
        _currentlyDrawing = false;
    }
}
