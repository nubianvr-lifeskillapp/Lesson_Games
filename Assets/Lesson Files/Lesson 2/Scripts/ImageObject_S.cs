using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "Image Object", menuName ="Scriptable Objects/Lesson 2/Image Object")]
public class ImageObject_S : ScriptableObject
{
    public Sprite image;
    public int imagePoint = 0;
    public string descriptionText;
    public Sound descriptionAudio;
}
