using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class MediaVCR : MonoBehaviour
{
    public MediaPlayer mediaPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayVideo()
    {
        mediaPlayer.Play();
    }
    
    public void OnRewindButton()
    {
        mediaPlayer.Control.Rewind();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
