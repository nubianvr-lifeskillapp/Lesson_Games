using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "New Question", menuName = "Scriptable Objects/Lesson 5/Question")]

public class Questions : ScriptableObject
{
    public Sprite questionImage;
    public bool isClickTrue;
    
    [MultiLineProperty(10)]
    public string questionText;
    
    public string trueAnswerText;
    public string falseAnswerText;
    
    [Title("True Commentary Statement", bold: false)]
    [HideLabel]
    [MultiLineProperty(5)]
    public string trueCommentaryStatement;
    
    [Title("False Commentary Statement", bold: false)]
    [HideLabel]
    [MultiLineProperty(5)]
    public string falseCommentaryStatement;
    
    public string senderName;
    public Sprite senderProfileImage;
}
