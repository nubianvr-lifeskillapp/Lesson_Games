using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class L8_GameManager : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private Questions[] questionObjects;
    Questions currentQuestion;
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
    private GameObject gameplayScreen;
    [SerializeField]
    private GameObject gameplayContinueButton;
    [SerializeField]
    private GameObject endScreen;

    [SerializeField] private Flowchart Flowchart;
    private int playerScore;
    public TMP_Text cluesSolvedNumberText;
    public GameObject[] boardElements;

    // Start is called before the first frame update
    void Start()
    {
        PlaySFX("BackgroundMusic");
        OverallGameManager.overallGameManager.playerData.currentLesson = SceneManager.GetActiveScene().buildIndex;
        playerScore = 0;
        cluesSolvedNumberText.text = $"{playerScore}/{questionObjects.Length}";

        clueboardUI.gameObject.SetActive(true);
        commentaryBox.gameObject.SetActive(false);
        questionBoxUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cluesSolvedNumberText.text = $"{playerScore}/{questionObjects.Length}";
    }

    public void SetQuestionBox(int index)
    {
        boardElements[index].SetActive(false);
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
            playerScore += 1;
            correctAnswers++;
            if (commentaryText)
            {
                commentaryText.text = currentQuestion.isClickTrue ? currentQuestion.trueCommentaryStatement : currentQuestion.falseCommentaryStatement;
            }
            
        }
        else
        {
            wrongAnswers++;
            if (commentaryText)
               commentaryText.text = currentQuestion.isClickTrue ? currentQuestion.falseCommentaryStatement : currentQuestion.trueCommentaryStatement;
        }
        noOfQuestionsAnswered++;
        trueButton.gameObject.SetActive(false);
        falseButton.gameObject.SetActive(false);
        if (commentaryBox)
            commentaryBox.gameObject.SetActive(true);

    }

    public void ShowScreen(GameObject screen)
    {
        gameplayScreen.SetActive(false);
    
        endScreen.SetActive(false);

        screen.SetActive(true);
    }

    public void showGameplayContinueButton()
    {
        if (noOfQuestionsAnswered < questionObjects.Length) return;
        Debug.Log("Completed");
        gameplayContinueButton.SetActive(true);
    }

    public void ExecuteAfterVideo()
    {
        Flowchart.ExecuteBlock("AfterVideo");
    }

    public void PlaySFX(string soundname)
    {
        SoundManager.soundManager.PlaySFX(soundname);
    }

    public void StopAllSFX()
    {
        SoundManager.soundManager.StopAllSFX();
    }

    public void RelaodScene()
    {
        OverallGameManager.overallGameManager.ReloadScene();
    }

    public void LoadNextScene(int scene)
    {
        OverallGameManager.overallGameManager.LoadNextScene(scene);
    }

    public void TriviaFinishedButton()
    {
        Flowchart.ExecuteBlock(playerScore < 3 ? "TriviaFinishedUnder3" : "TriviaFinishedOver3");
    }

    public void ResetGame()
    {
        foreach (var element in boardElements)
        {
            element.gameObject.SetActive(true);
        }

        playerScore = 0;
        noOfQuestionsAnswered = 0;
        correctAnswers = 0;
        wrongAnswers = 0;

    }
}
