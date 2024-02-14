using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class DrawingExporter : MonoBehaviour
{
    private AndroidPermissionHandler _permissionHandler;
    private GameObject[] _strokes;
    private string _path = Application.persistentDataPath;
    

    private void Start()
    { // in Brush.cs newStroke.tag = "Stroke" 
        _permissionHandler = new AndroidPermissionHandler();
        if (_permissionHandler.CheckStoragePermissions())
        {
            ObjectsToJSON();
        } else
        {
            Debug.Log("Permission for accessing storage not granted. Unable to save drawing");
        }
    }


    private void ObjectsToJSON()
    {
        _strokes = GameObject.FindGameObjectsWithTag("Stroke");
        var sb = new StringBuilder();

        foreach (GameObject stroke in _strokes)
        {
            string json = JsonUtility.ToJson(stroke);
            sb.AppendLine(json);
        }

        File.WriteAllText(FilePath(), sb.ToString());
    }
    
    private string FilePath()
    {
        string Path = _path + "/drawing" + System.DateTime.Now.ToString("yyyyMMddHHmm") + ".json";
        Debug.Log("Path: " + Path);
        return Path;
    }

    
}
