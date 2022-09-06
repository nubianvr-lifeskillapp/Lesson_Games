using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class L9_GameManager : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private Questions[] questionObjects;
    int questionIndex = 0;
    public int correctAnswers = 0;
    public int wrongAnswers = 0;
    // Question Image is Post Image...
    // Question Text is SenderCommentText...

    [SerializeField]
    private Image postImage;
    [SerializeField]
    private Image senderProfileImage;
    [SerializeField]
    private TMP_Text senderCommentText;
    [SerializeField]
    private TMP_Text senderUsernameText;

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

    public int noOfQuestionsAnswered = 0;

    private bool isCorrectAnalysis = false;

    // Start is called before the first frame update
    void Start()
    {
        gameplayContinueButton.SetActive(false);
        ShowScreen(introScreen);
        SetQuestionBox();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetQuestionBox()
    {
        postImage.sprite = questionObjects[questionIndex].questionImage;
        senderProfileImage.sprite = questionObjects[questionIndex].senderProfileImage;
        senderCommentText.text = questionObjects[questionIndex].questionText;
        senderUsernameText.text = questionObjects[questionIndex].senderName;
    }

    public void CheckSelection(bool condition)
    {
        noOfQuestionsAnswered++;
        if (condition == questionObjects[questionIndex].isClickTrue)
        {
            //Execute true statement...
            correctAnswers++;
            Debug.Log("Correct!");
        }
        else
        {
            //Execute false statement...
            wrongAnswers++;
            Debug.Log("Incorrect!");
        }

        // Increment Question Index...
        questionIndex++;
        if (!(questionIndex < questionObjects.Length))
        {
            // Game Over
            questionIndex = 0;
            SetQuestionBox();
        }
        else
        {
            // Set new question set...
            SetQuestionBox();
        }
        ShowGameplayContinueButton();

        // Set isCorrectAnalysis when at last question...
        if (noOfQuestionsAnswered >= questionObjects.Length)
        {
            if (condition == questionObjects[(questionObjects.Length - 1)].isClickTrue)
            {
                isCorrectAnalysis = true;
                Debug.Log("Spot On!");
            }
        }
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

    public void ShowGameplayContinueButton()
    {
        if (noOfQuestionsAnswered >= questionObjects.Length)
        {
            Debug.Log("Completed");
            gameplayContinueButton.SetActive(true);
        }
    }
}
