using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection.Emit;
using Fungus;
using UnityEngine.SceneManagement;

public class L1_GameManager : MonoBehaviour,ICustomMessengerScript
{

    public InputField inputField;
    public TMP_Text errorText;
    [SerializeField]
    private TMP_Text usernameText;
    [SerializeField]
    private TMP_Text endUsernameText;
    [SerializeField]
    private L1_UIManager uIManager;
    [SerializeField]
    private Button qB_ContinueButton;

    [Header("Question Properties")]
    [SerializeField]
    private Questions[] allQuestions;
    public int noOfQuestions = 0;
    private static int questionIndex = 0;
    private int questionCount = 0;
    private int noOfQuestionsAnswered = 0;
    private int correctAnswers = 0;
    private int wrongAnswers = 0;
    public bool isLevelFinished = false;
    //public static L1_GameManager gameManager;
    public int maxUsernameLength = 6;
    private string username;
    public Flowchart flowchart;
    private int playerPoints;
    
    

    //Awake Method
    private void Awake()
    {
       
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        OverallGameManager.overallGameManager.playerData.currentLesson = SceneManager.GetActiveScene().buildIndex;
        SoundManager.soundManager.PlaySFX("BackgroundMusic");
        errorText.gameObject.SetActive(false);
        //Set question on scene start...
        uIManager.SetQuestionElements(allQuestions[questionIndex]);
        questionCount = allQuestions.Length;
        noOfQuestions = allQuestions.Length;
        Debug.Log("No of Questions: " + noOfQuestions);
        playerPoints = 0;
        //DataPersistanceManager.instance.SaveStudentGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnterUsername()
    {
        username = inputField.text;


        if (username.Length < maxUsernameLength)
        {
            errorText.gameObject.SetActive(true);
            //Debug.Log("Input text not up to 6 characters...");
        }
        else
        {
          
           if(isLevelFinished)
           {
               if (usernameText)
                   endUsernameText.text = "@" + username;
               DisableInputUI();
               flowchart.ExecuteBlock("End UI");
               //uIManager.SetUIActive(uIManager.endUI);
               
           }
           else
           {
               if (usernameText)
                   usernameText.text = "@" + username;
               DisableInputUI();
               flowchart.ExecuteBlock("Input UI Block");
               //uIManager.SetUIActive(uIManager.proceedUI);
           }
        }
    }

    private void DisableInputUI()
    {
        //Disable input UI...
        uIManager.ShowInputUI(false);
    }

    private void runSendUsername ()
    {
        Debug.Log("Username Sent");   

    }

    public void CheckSelection(bool condition)
    {
        if (!(uIManager.TrueAnswerButton && uIManager.FalseAnswerButton))
            return;

        //If it is the right selection...
        if (allQuestions[questionIndex].isClickTrue == condition)
        {
            flowchart.ExecuteBlock("Fade Question Out");
            uIManager.ShowAffirmationText("Correct!");
            uIManager.IncrementPointLocation();
            
            correctAnswers++;
            playerPoints += 25;
            Debug.Log("Correct");
            SoundManager.soundManager.PlaySFX("CorrectAccelerate");
        }

        //If it is the wrong selection....
        else
        {
            flowchart.ExecuteBlock("Fade Question Out");
            uIManager.ShowAffirmationText("Incorrect!");
            SoundManager.soundManager.PlaySFX("IncorrectIdle");
            wrongAnswers++;
            Debug.Log("Incorrect");
        }

        flowchart.SetIntegerVariable("PlayerPoints", playerPoints );
        MoveToNextQuestion();
    }
    public void MoveToNextQuestion()
    {
        //Increase Number of Questions Answered...
        noOfQuestionsAnswered++;
        //Increase questionIndex...
        if (questionIndex < allQuestions.Length - 1)
        {
            questionIndex++;
            uIManager.ShowQuestionUI(false, 0.0f);
            uIManager.ShowQuestionUI(true, 1.0f);
            flowchart.ExecuteBlock("Fade Question In");
            uIManager.SetQuestionElements(allQuestions[questionIndex]);
        }

        //Reset to first question index...
        else
        {
            flowchart.ExecuteBlock("Fade Question In");
            OnLevelFinshed();
        }
    }

    public void OnLevelFinshed()
    {
        isLevelFinished = true;
        questionIndex = 0;
        uIManager.questionText.gameObject.SetActive(false);
        uIManager.FalseAnswerButton.gameObject.SetActive(false);
        uIManager.TrueAnswerButton.gameObject.SetActive(false);
        uIManager.questionTextBackground.gameObject.SetActive(false);
        qB_ContinueButton.gameObject.SetActive(true);
        //StartCoroutine(ShowResultsMenu(true));
    }

    public void ReloadScene()
    {
        OverallGameManager.overallGameManager.ReloadScene();
    }

    public void LoadNextScene(int scene)
    {
        OverallGameManager.overallGameManager.LoadNextScene(scene);
    }

    public void SetLevelFinished(bool condition)
    {
        isLevelFinished = condition;
    }

    public void SetErrorMessageInactive()
    {
        errorText.gameObject.SetActive(false);
    }

    public void FinishedPlayingVideo()
    {
        flowchart.ExecuteBlock("Continue After Video");
    }

    public void IntroVideoPlayEnded()
    {
        OverallGameManager.overallGameManager.playerData.currentLesson = SceneManager.GetActiveScene().buildIndex;
        flowchart.ExecuteBlock("After Intro Video Game Started");
    }

    public void PlayButtonSFX(string soundName)
    {
        SoundManager.soundManager.PlaySFX(soundName);
    }

    public void StopAllSFX()
    {
        SoundManager.soundManager.StopAllSFX();
    }
    
}
