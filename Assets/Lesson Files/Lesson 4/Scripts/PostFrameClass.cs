using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostFrameClass : MonoBehaviour
{
    public PostFramescriptable postFrame;
    public Image postFrameImage;
    private bool ImageReviewed = false;

    public bool publicImageReviewed
    {
        get => ImageReviewed;
        set => ImageReviewed = value;
    }

    private void OnEnable()
    {
        if(postFrame)
            postFrameImage.sprite = postFrame.image;
    }
    


    public void SetImage(Sprite image)
    {
        postFrameImage.sprite = image;
    }
}
