using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName ="TriviaQuestionObject", menuName ="Scriptable Objects/TriviaQuestionObject")]
public class TriviaQuestionObject : ScriptableObject
{
    //Properties...
    [MultiLineProperty(5)]
    public string question;
    [MultiLineProperty(5)]
    public string[] answers;
    public int correctAnswerIndex;
    public int points;
}
