using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "New Question", menuName = "Scriptable Objects/Lesson 5/Question")]
public class Questions : ScriptableObject
{
    public bool isClickTrue;
    public string textQuestion;
    public string trueAnswerText;
    public string falseAnswerText;
}
