using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;

public class DrawingImporter : MonoBehaviour
{
    private string[] _objectsToCreate;

    private FileBrowser.Permission _storagePermission;

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

    [ContextMenu("LoadDrawing")]
    public void ImportDrawing()
    {
        if(StoragePermission())
        {
            try
            {
                FindFile();
                CreateAllObjectsFromFile();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                return;
            }
        } else
        {
            Debug.Log("No permission to access storage");
            return;
        }
    }

    // opens file explorer
    private void FindFile()
    {
        try
        {
            FileBrowser.ShowLoadDialog((path) => { SplitJson(path); }, null, 0, false, null, null, "Select File", "Select");
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            throw;
        }
    }
    
    private void CreateAllObjectsFromFile()
    {
        foreach (string obj in _objectsToCreate)
        {
            CreateObjectFromJson(obj + "}");
        }
    }

    private void SplitJson(string[] filepath)
    {
        try
        {
            string text = File.ReadAllText(filepath[0]);
            _objectsToCreate = text.Split('}');
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            throw;
        }
    }

    private void CreateObjectFromJson(string json)
    {
        GameObject newstroke = JsonUtility.FromJson<GameObject>(json);
        newstroke.tag = "Stroke";
    }

    void Start()
    {

    }

    void Update()
    {

    }

}
