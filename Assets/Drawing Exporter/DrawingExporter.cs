using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using SimpleFileBrowser;

public class DrawingExporter : MonoBehaviour
{
    public GameObject _exportDrawingButton;

    private GameObject[] _strokeObjects;
   // private string _path = Application.persistentDataPath;
    private string _jsonText = "";

    private FileBrowser.Permission _storagePermission;

    private void Start()
    { // in Brush.cs newStroke.tag = "Stroke" 
        
    }

    private bool StoragePermission()
    {
        _storagePermission = FileBrowser.CheckPermission();
        if (_storagePermission == FileBrowser.Permission.ShouldAsk)
        {
            FileBrowser.RequestPermission();
            _storagePermission = FileBrowser.CheckPermission();
        }
        return _storagePermission == FileBrowser.Permission.Granted;
    }

    [ContextMenu("Save Drawing")]
    public void SaveDrawing()
    {
        Debug.Log("Saving Drawing");
        if(StoragePermission())
        {
            Debug.Log("Yay! We got permission");
            FindAllStrokes();
            StrokeObjectsToJSON();
            FindSaveLocation();
        } else
        {
            Debug.Log("No permission to access storage");
            return;
        }  
    }

    private void FindAllStrokes()
    {
        _strokeObjects = GameObject.FindGameObjectsWithTag("Stroke");
    }

    private void StrokeObjectsToJSON()
    {
        foreach (GameObject stroke in _strokeObjects)
        {
            _jsonText += JsonUtility.ToJson(stroke);
        }
    }
    
    private void FindSaveLocation()
    {
        FileBrowser.ShowSaveDialog((path) => { SaveFile(path); }, null, 0, false, null, DefaultFileName(), "Save", "Save"); 
    }

    private void SaveFile(string[] path)
    {
        try
        {
            File.WriteAllText(path[0], _jsonText);
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
            return;
        }
    }

    // creates a filename with the current date & time
    private string DefaultFileName()
    {
        return "drawing" + System.DateTime.Now.ToString("yyyyMMddHHmm") + ".json";
    }
    
}
