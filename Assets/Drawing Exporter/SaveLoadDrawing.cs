using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadDrawing
{
    public Stroke _stroke;
    public float posX;
    public float posY;
    public float posZ;

    public SaveLoadDrawing(GameObject obj)
    {
        _stroke = obj.GetComponent<Stroke>();
        posX = obj.transform.position.x;
        posY = obj.transform.position.y;
        posZ = obj.transform.position.z;
    }

    public GameObject ConfigureGameObject()
    {
        GameObject obj = new GameObject();
        Vector3 pos = new Vector3(posX, posY, posZ);
        _stroke = obj.AddComponent<Stroke>();
        obj.transform.position = pos;
        return obj;
    }

}


