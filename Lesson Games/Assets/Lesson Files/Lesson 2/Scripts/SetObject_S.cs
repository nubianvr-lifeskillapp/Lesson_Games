using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Image", menuName ="Scriptable Objects/Lesson 2/Set Object")]
public class SetObject_S: ScriptableObject
{
    public ImageObject_S[] backgroundImages;
    public ImageObject_S[] middlegroundImages;
    public ImageObject_S[] foregroundImages;
}
