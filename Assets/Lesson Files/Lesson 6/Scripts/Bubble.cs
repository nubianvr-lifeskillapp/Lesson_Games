using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bubble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MoveBubble();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveBubble()
    {
        gameObject.transform.DOMoveY(gameObject.transform.position.y + 200, 0.75f);
    }
}
