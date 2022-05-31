using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Lesson 2 Properties...
    [DllImport("__Internal")]
    private static extern void printPoints(int score);
    [Header("Lesson Two Properties")]
    [SerializeField]
    private TMP_Text scoreText;
    #endregion
    #region Lesson 5 Properties...
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
    #endregion
    #region Lesson 7 Properties...
    [Header("Lesson Seven Properties")]
    [SerializeField]
    private TMP_Text coinText;
    [SerializeField]
    private RectTransform gameOverLayout;
    #endregion
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        #region For Lesson 5...
        ShowQuestionUI(false);
        ShowResultsMenu(false);
        #endregion
        #region For Lesson 7...
        if(gameOverLayout)
            gameOverLayout.gameObject.SetActive(false);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Lesson 2 Functions...
    public void SetScoreText()
    {
        if (scoreText)
        {
            scoreText.text = "Score: " + GameManager.gameManager.totalPoints;
            scoreText.gameObject.SetActive(true);

#if UNITY_WEBGL == true && UNITY_EDITOR == false
    printPoints ( GameManager.gameManager.totalPoints);
#endif
        }
    }
    #endregion
    #region Lesson 5 Functions...
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
        if(AffirmationText)
        {
            AffirmationText.gameObject.SetActive(true);
            //Set text...
            AffirmationText.text = text;
            //Hide AffirmationText after 2.0s...
            Invoke(nameof(HideAffirmationText), 2.0f);
        }
    }

    private void HideAffirmationText()
    {
        AffirmationText.gameObject.SetActive(false);
    }

    public void ShowResultsMenu(bool condition)
    {
        if(QuestionsAskedText)
            QuestionsAskedText.text = "Number Of Questions Asked : " + GameManager.gameManager.noOfQuestions;
        if(QuestionsAnsweredText)
            QuestionsAnsweredText.text = "Number Of Questions Answered : " + GameManager.gameManager.noOfQuestionsAnswered;
        if(CorrectAnswersText)
            CorrectAnswersText.text = "Correct Answers : " + GameManager.gameManager.correctAnswers;
        if(WrongAnswersText)
            WrongAnswersText.text = "Wrong Answers : " + (GameManager.gameManager.wrongAnswers + (GameManager.gameManager.noOfQuestions - GameManager.gameManager.noOfQuestionsAnswered));
        if(ResultsMenuUI)
            ResultsMenuUI.gameObject.SetActive(condition);
    }
    #endregion
    #region Lesson 7 Properties...
    public void SetCoinText(int value)
    {
        if (coinText)
        {
            coinText.text = "Coins: " + value.ToString();
        }
    }

    public void ClosePopupLayout(Button button)
    {
        button.transform.parent.parent.gameObject.SetActive(false);
        GameManager.gameManager.IncreaseNoOfPopupsClosed();
    }

    public void ShowGameOverLayout()
    {
        if (gameOverLayout)
        {
            gameOverLayout.gameObject.SetActive(true);
        }
    }
    #endregion
}
