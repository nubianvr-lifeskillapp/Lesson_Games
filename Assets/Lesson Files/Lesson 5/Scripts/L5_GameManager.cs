using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Fungus;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class L5_GameManager : MonoBehaviour
{
    //Properties....
    [Header("Head Properties")]
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private TimeManager timeManager;
    [SerializeField]
    private L5_UIManager uIManager;
    
    [Header("Question Properties")]
    [SerializeField]
    private Question5Objects[] allQuestions;

    public int noOfQuestions = 0;
    private int questionIndex = 0;
    private int questionCount = 0;
    public int noOfQuestionsAnswered = 0;
    public int correctAnswers = 0;
    public int wrongAnswers = 0;
    public bool isLevelFinished = false;
    public bool questionAnswered;
    private List<Question5Objects> unansweredQuestion;
    public static L5_GameManager gameManager;
    private int RandomInt;
    public int NumberOfQuestionsToAnswer;
    public Flowchart Flowchart;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        OverallGameManager.overallGameManager.playerData.currentLesson = SceneManager.GetActiveScene().buildIndex;
        unansweredQuestion = allQuestions.ToList();
        SetQuestion();
    }

    public void SetQuestion()
    {
        RandomInt = Random.Range(0, unansweredQuestion.Count);
        print(RandomInt);
        uIManager.SetQuizElements(unansweredQuestion[RandomInt]);
        Debug.Log("No of Questions: " + allQuestions.Length);
        questionCount = allQuestions.Length;
        noOfQuestions = allQuestions.Length;
    }

    public void CheckSelection(AnswerEnumClass btnValue)
    {
        //If it is the right selection...
        if(unansweredQuestion[RandomInt].correctAnswer == btnValue.answer)
        {
            uIManager.ShowAffirmationText("Correct!");
            uIManager.ShowQuestionUI(false);
            player.Jump();
            correctAnswers++;
            questionAnswered = true;
        }

        //If it is the wrong selection....
        else
        {
            uIManager.ShowAffirmationText("Incorrect!");
            uIManager.ShowQuestionUI(false);
            wrongAnswers++;
        }

        
        noOfQuestionsAnswered++;
        timeManager.bCanScaleUp = true;
        MoveToNextQuestion();
    }

    public void MoveToNextQuestion()
    {
        unansweredQuestion.Remove(unansweredQuestion[RandomInt]);
        //Increase questionIndex...
        if (noOfQuestionsAnswered != NumberOfQuestionsToAnswer)
        {
            //Set Quiz UI Elements...
            //uIManager.SetQuizElements(allQuestions[questionIndex]);
            //questionAnswered = false;
            SetQuestion();
            StartCoroutine(WaitForJump());
        }
          
        //Reset to first question index...
        else
        {
            OnLevelFinshed();
        }
    }

    public void ReloadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    IEnumerator ShowResultsMenu(bool condition)
    {
        yield return new WaitForSeconds(3.0f);
        uIManager.ShowResultsMenu(condition);
        player.isRunning = false;
    }

    private IEnumerator ShowLevelFailed()
    {
        yield return new WaitForSeconds(3.0f);
        uIManager.ShowFailedScreen();
        player.isRunning = false;
    }

    IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(1.75f);
        questionAnswered = false;
    }

    public void ResetGame()
    {
        unansweredQuestion.Clear();
        unansweredQuestion = allQuestions.ToList();
        player.isRunning = true;
        noOfQuestionsAnswered = 0;
        player.playerIsDead = false;
        player.playerLife = 3;
        correctAnswers = 0;
        wrongAnswers = 0;
        uIManager.ShowResultsMenu(false);
        uIManager.FailedMenuUI.DOFade(0, 0.75f);
        uIManager.FailedMenuUI.gameObject.SetActive(false);
        isLevelFinished = false;
        SetQuestion();
    }

    public void OnPlayerDead()
    {
        isLevelFinished = true;
        StartCoroutine(ShowLevelFailed());
    }

    public void OnLevelFinshed()
    {
        isLevelFinished = true;
        StartCoroutine(ShowResultsMenu(true));
    }

    public void PlaySFX(string soundName)
    {
        SoundManager.soundManager.PlaySFX(soundName);
    }

    public void StopAllSFX()
    {
        SoundManager.soundManager.StopAllSFX();
    }
    
    public void ExecuteAfterVideo()
    {
        Flowchart.ExecuteBlock("ExecuteAfterVideo");
    }
    
    public void StartRunning()
    {
        player.isRunning = true;
    }

    public void Restart()
    {
        OverallGameManager.overallGameManager.ReloadScene();
    }

    public void NextLesson(int buildIndex)
    {
        OverallGameManager.overallGameManager.LoadNextScene(buildIndex);
    }
}

