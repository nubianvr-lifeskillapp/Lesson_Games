using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum AnswerOptions
{
    FakeNews,
    ViolentContent,
    SelfHarmContent,
}

[CreateAssetMenu(fileName = "New Question", menuName = "Scriptable Objects/Lesson 5/Question Object")]
public class Question5Objects : ScriptableObject
{
    public string[] answers = new []{"Fake News", "Violent Crimes", "Self-Harm Content"};
    public Sprite questionImage;
    public string questionText;
    public AnswerOptions correctAnswer;
}
