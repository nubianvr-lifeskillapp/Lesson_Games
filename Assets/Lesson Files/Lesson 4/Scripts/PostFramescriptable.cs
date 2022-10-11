using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Post Frame", menuName ="Scriptable Objects/Lesson 4/Post Frame Object")]
public class PostFramescriptable : ScriptableObject
{
    public Sprite image;
    public int imagePoint = 0;
    public bool isImageGood;

}

