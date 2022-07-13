using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriviaSystem : MonoBehaviour
{
    //Array to hold all trivia question object...
    [SerializeField]
    private TriviaQuestionObject[] questionObjects;

    private int noOfQuestions;
    private int noOfCorrectAnswers;
    private int noOfWrongAnswers;

    // A variable to keep track of the question index...
    private int questionIndex = 0;

    // Array holding reference to all buttons on the trivia layout...
    [SerializeField]
    private TMP_Text[] answerTexts;

    // Array holding reference to question text ui...
    [SerializeField] TMP_Text questionText;
    [SerializeField]
    private TMP_Text commentaryText;

    // Start is called before the first frame update
    void Start()
    {
        noOfQuestions = questionObjects.Length;
        SetUIElements();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SetUIElements()
    {
        commentaryText.gameObject.SetActive(false);
        questionText.text = questionObjects[questionIndex].question;
        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = questionObjects[questionIndex].answers[i];
        }
    }
    public void CheckSelection(int index)
    {
        if (!commentaryText)
            return;
        commentaryText.gameObject.SetActive(true);
        if (index == questionObjects[questionIndex].correctAnswerIndex)
        {
            Debug.Log("Correct Answer!!!");
            commentaryText.text = "Correct!";
            noOfCorrectAnswers++;
        }
        else
        {
            Debug.Log("Incorrect Answer!!!");
            commentaryText.text = "Incorrect!";
            noOfWrongAnswers++;
        }



        //MoveToNextQuestion...
        if(questionIndex < questionObjects.Length-1)
            questionIndex++;
        else
            questionIndex = 0;
        Invoke(nameof(SetUIElements), 1.0f);
    }
}
