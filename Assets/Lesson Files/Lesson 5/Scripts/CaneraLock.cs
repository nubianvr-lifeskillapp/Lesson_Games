using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneraLock : MonoBehaviour
{

    private float cameraPositionY;
    
    // Start is called before the first frame update
    void Start()
    { 
        cameraPositionY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        position = new Vector3(position.x,cameraPositionY,position.z);
        transform.position = position;
    }
}
