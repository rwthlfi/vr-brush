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
        _size = 1.0f;
        _color = Color.green;
        _currentlyDrawing = false;
    }

    // Update is called once per frame
    void Update()
    {
        _triggerPressed = _rightHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out _triggerPressed) && _triggerPressed;
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
        _currentStroke.AddPoint(this.transform.position);
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
