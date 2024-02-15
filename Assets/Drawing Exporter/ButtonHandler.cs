using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject _saveButton;
    public GameObject _loadButton;
    
    private DrawingImporter _drawingImporter;
    private DrawingExporter _drawingExporter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickSave()
    {
        _drawingExporter.SaveDrawing();
    }

    public void ClickImport()
    {
        _drawingImporter.ImportDrawing();
    }
}
