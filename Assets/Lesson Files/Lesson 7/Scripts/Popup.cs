using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Popup", menuName ="Scriptable Objects/Lesson 7/Popup")]
public class Popup : ScriptableObject
{
    public Sprite popupImage;
    public int coins = 0;
    public bool isHarmful = false;
}
