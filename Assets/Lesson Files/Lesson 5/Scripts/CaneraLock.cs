using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class CaneraLock : MonoBehaviour
{

    private float cameraPositionY;
    public Flowchart Flowchart;
    
    // Start is called before the first frame update
    void Start()
    { 
        cameraPositionY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Flowchart.GetBooleanVariable("CameraLocked")) return;
        var position = transform.position;
        position = new Vector3(position.x,cameraPositionY,position.z);
        transform.position = position;


    }
}
