using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Question", menuName = "Scriptable Objects/Lesson 5/Question")]
public class Questions : ScriptableObject
{
    public Sprite questionImage;
    public bool isClickTrue;
    public string textQuestion;
    public string trueAnswerText;
    public string falseAnswerText;
    public string trueCommentaryStatement;
    public string falseCommentaryStatement;
    public string senderName;
    public Sprite senderProfileImage;
    public Sprite userProfileImage;

}
