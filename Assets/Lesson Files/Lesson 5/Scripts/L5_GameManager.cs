using System.Collections;
using System.Collections.Generic;
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
    private Questions[] allQuestions;

    public int noOfQuestions = 0;
    private int questionIndex = 0;
    private int questionCount = 0;
    public int noOfQuestionsAnswered = 0;
    public int correctAnswers = 0;
    public int wrongAnswers = 0;
    public bool isLevelFinished = false;

    public static L5_GameManager gameManager;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        uIManager.SetQuizElements(allQuestions[questionIndex]);
        Debug.Log("No of Questions: " + allQuestions.Length);
        questionCount = allQuestions.Length;
        noOfQuestions = allQuestions.Length;
    }

    public void CheckSelection(bool condition)
    {
        if (!(uIManager.TrueAnswerButton && uIManager.FalseAnswerButton))
            return;

        //If it is the right selection...
        if(allQuestions[questionIndex].isClickTrue == condition)
        {
            uIManager.ShowAffirmationText("Correct!");
            uIManager.ShowQuestionUI(false);
            player.Jump();
            correctAnswers++;
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
        //Increase questionIndex...
        if (questionIndex < allQuestions.Length - 1)
        {
            questionIndex++;
            //Set Quiz UI Elements...
            uIManager.SetQuizElements(allQuestions[questionIndex]);
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
    }

    public void OnLevelFinshed()
    {
        isLevelFinished = true;
        StartCoroutine(ShowResultsMenu(true));
    }
}

