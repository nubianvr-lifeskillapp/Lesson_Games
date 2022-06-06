using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class L1_UIManager : MonoBehaviour
{
    //Properties...
    [Header("Question Properties")]
    [SerializeField]
    private RectTransform inputUI;
    [SerializeField]
    private Transform questionBoxUI;
    [SerializeField]
    private Transform ResultsMenuUI;
    [SerializeField]
    private Image point;
    [SerializeField]
    private float pointParentWidth;

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
        //ShowQuestionUI(false);
        //ShowResultsMenu(false);
        AffirmationText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetQuestionElements(Questions question)
    {
        if (questionText && trueAnswerText && falseAnswerText)
        {
            questionText.text = question.textQuestion;
            trueAnswerText.text = question.trueAnswerText;
            falseAnswerText.text = question.falseAnswerText;
        }
    }

    public void ShowQuestionUI(bool condition, float time)
    {
        //if (questionBoxUI)
        //    questionBoxUI.gameObject.SetActive(condition);
        StartCoroutine(SetQuestionUIActive(condition, time));
    }
    private IEnumerator SetQuestionUIActive(bool condition, float time)
    {
        yield return new WaitForSeconds(time);
        if (questionBoxUI)
            questionBoxUI.gameObject.SetActive(condition);
    }
    public void ShowInputUI(bool condition)
    {
        if(inputUI)
            inputUI.gameObject.SetActive(condition);
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

    public void IncrementPointLocation()
    {
        if (!point)
            return;
        point.rectTransform.anchoredPosition += new Vector2(pointParentWidth / L1_GameManager.gameManager.noOfQuestions, 0);
    }

    public void ShowResultsMenu(bool condition)
    {
        ShowQuestionUI(false, 0.0f);
        ResultsMenuUI.gameObject.SetActive(condition);
        QuestionsAskedText.text = "Number Of Questions Asked : " + L1_GameManager.gameManager.noOfQuestions;
        QuestionsAnsweredText.text = "Number Of Questions Answered : " + L1_GameManager.gameManager.noOfQuestionsAnswered;
        CorrectAnswersText.text = "Correct Answers : " + L1_GameManager.gameManager.correctAnswers;
        WrongAnswersText.text = "Wrong Answers : " + (L1_GameManager.gameManager.wrongAnswers + (L1_GameManager.gameManager.noOfQuestions - L1_GameManager.gameManager.noOfQuestionsAnswered));
    }
}
