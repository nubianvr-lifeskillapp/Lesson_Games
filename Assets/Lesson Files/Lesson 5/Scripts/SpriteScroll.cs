using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroll : MonoBehaviour
{

    public float parallaxEffect;

    public float startPos;

    public float lengthOfSprite;

    public float temp;
    public float offset;

    private GameObject _camera;
    // Start is called before the first frame update

    private void Awake()
    {
        _camera = GameObject.Find("Main Camera");
    }

    void Start()
    
    {
        startPos = gameObject.transform.position.x;
        lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        temp = (_camera.transform.position.x * (1 - parallaxEffect));
        float distance = (_camera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + lengthOfSprite)
        {
            startPos += lengthOfSprite;
            startPos += offset;
        }
    }


}
