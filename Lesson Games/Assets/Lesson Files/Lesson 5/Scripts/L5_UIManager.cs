using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class L5_UIManager : MonoBehaviour
{
    //Properties...
    [Header("Question Properties")]
    [SerializeField]
    private Transform questionBoxUI;
    [SerializeField]
    private Transform ResultsMenuUI;
   
    public TMP_Text questionText;
    public TMP_Text trueAnswerText;
    public TMP_Text falseAnswerText;
    public Button TrueAnswerButton;
    public Button FalseAnswerButton;
    public TMP_Text AffirmationText;
    public TMP_Text QuestionsAskedText;
    public TMP_Text QuestionsAnsweredText;
    public TMP_Text CorrectAnswersText;
    public TMP_Text WrongAnswersText;

    // Start is called before the first frame update
    void Start()
    {
        ShowQuestionUI(false);
        ShowResultsMenu(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetQuizElements(Questions question)
    {
        if (questionText && trueAnswerText && falseAnswerText)
        {
            questionText.text = question.textQuestion;
            trueAnswerText.text = question.trueAnswerText;
            falseAnswerText.text = question.falseAnswerText;
        }
    }

    public void ShowQuestionUI(bool condition)
    {
        if(questionBoxUI)
           questionBoxUI.gameObject.SetActive(condition);
    }

    public void ShowAffirmationText(string text)
    {
        //Set object active(Visible on screen)...
        AffirmationText.gameObject.SetActive(true);
        //Set text...
        AffirmationText.text = text;
        //Hide AffirmationText after 2.0s...
        Invoke(nameof(HideAffirmationText), 2.0f);
    }

    private void HideAffirmationText()
    {
        AffirmationText.gameObject.SetActive(false);
    }

    public void ShowResultsMenu(bool condition)
    {
        QuestionsAskedText.text = "Number Of Questions Asked : " + GameManager.gameManager.noOfQuestions;
        QuestionsAnsweredText.text = "Number Of Questions Answered : " + GameManager.gameManager.noOfQuestionsAnswered;
        CorrectAnswersText.text = "Correct Answers : " + GameManager.gameManager.correctAnswers;
        WrongAnswersText.text = "Wrong Answers : " + (GameManager.gameManager.wrongAnswers + (GameManager.gameManager.noOfQuestions - GameManager.gameManager.noOfQuestionsAnswered));
        ResultsMenuUI.gameObject.SetActive(condition);
    }
}
