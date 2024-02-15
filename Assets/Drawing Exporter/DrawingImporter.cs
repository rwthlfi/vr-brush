using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;

public class DrawingImporter : MonoBehaviour
{
    public GameObject _importDrawingButton;

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
        Debug.Log("Loading Drawing");
        if(StoragePermission())
        {
            try
            {
                FindFile();
                CreateAllObjectsFromFile();
                Debug.Log("recreating drawing done now apparently");
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
            Debug.Log("Extracted contents from file");
            _objectsToCreate = text.Split('}');
            Debug.Log("Got json representation of objects");
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            throw;
        }
    }

    private void CreateObjectFromJson(string json)
    {
        SaveLoadDrawing drawingData = JsonUtility.FromJson<SaveLoadDrawing>(json);
        ConfigureGameObject(drawingData);
    }

    private void ConfigureGameObject(SaveLoadDrawing drawingData)
    {
        GameObject newStroke = new GameObject();
        drawingData._stroke = newStroke.AddComponent<Stroke>();
        Vector3 pos = new Vector3(drawingData.posX, drawingData.posY, drawingData.posZ);
        newStroke.transform.position = pos;
        newStroke.tag = "Stroke";
    }

    void Start()
    {

    }

    void Update()
    {

    }

}
