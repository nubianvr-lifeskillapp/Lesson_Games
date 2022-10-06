using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class L8_GameManager : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private Questions[] questionObjects;
    Questions currentQuestion;
    public int noOfQuestions = 0;
    private int questionCount = 0;
    public int noOfQuestionsAnswered = 0;
    public int correctAnswers = 0;
    public int wrongAnswers = 0;
    public bool isLevelFinished = false;

    [SerializeField]
    private RectTransform clueboardUI;
    [SerializeField]
    private RectTransform questionBoxUI;
    [SerializeField]
    private RectTransform commentaryBox;
    [SerializeField]
    private TMP_Text questionText;
    [SerializeField]
    private TMP_Text commentaryText;
    [SerializeField]
    private Image questionImage;
    [SerializeField]
    private Button trueButton;
    [SerializeField]
    private Button falseButton;

    [SerializeField]
    private GameObject introScreen;
    [SerializeField]
    private GameObject gameplayScreen;
    [SerializeField]
    private GameObject gameplayContinueButton;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject videoScreen;
    [SerializeField]
    private GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        ShowScreen(introScreen);
        noOfQuestions = questionObjects.Length;

        clueboardUI.gameObject.SetActive(true);
        commentaryBox.gameObject.SetActive(false);
        questionBoxUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetQuestionBox(int index)
    {
        currentQuestion = questionObjects[index];
        if (questionImage)
            questionImage.sprite = currentQuestion.questionImage;
        if (questionText)
            questionText.text = currentQuestion.questionText;
        if (clueboardUI)
            clueboardUI.gameObject.SetActive(false);
        if (questionBoxUI)
            questionBoxUI.gameObject.SetActive(true);
        trueButton.gameObject.SetActive(true);
        falseButton.gameObject.SetActive(true);
    }

    public void CheckSelection(bool condition)
    {
        if (currentQuestion.isClickTrue == condition)
        {
            correctAnswers++;
            if (commentaryText)
                commentaryText.text = currentQuestion.trueCommentaryStatement;
        }
        else
        {
            wrongAnswers++;
            if (commentaryText)
                commentaryText.text = currentQuestion.falseCommentaryStatement;
        }
        noOfQuestionsAnswered++;
        trueButton.gameObject.SetActive(false);
        falseButton.gameObject.SetActive(false);
        if (commentaryBox)
            commentaryBox.gameObject.SetActive(true);

    }

    public void ShowScreen(GameObject screen)
    {
        introScreen.SetActive(false);
        gameplayScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        videoScreen.SetActive(false);
        endScreen.SetActive(false);

        screen.SetActive(true);
    }

    public void showGameplayContinueButton()
    {
        if (noOfQuestionsAnswered >= questionObjects.Length)
        {
            Debug.Log("Completed");
            gameplayContinueButton.SetActive(true);
        }
    }
}