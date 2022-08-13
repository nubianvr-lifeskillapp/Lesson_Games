using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TriviaQuestionObject", menuName ="Scriptable Objects/TriviaQuestionObject")]
public class TriviaQuestionObject : ScriptableObject
{
    //Properties...
    public string question;
    public string[] answers;
    public int correctAnswerIndex;
    public int points;
}
