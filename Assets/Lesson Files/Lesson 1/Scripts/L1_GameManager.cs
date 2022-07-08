using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class L1_GameManager : MonoBehaviour
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
    public int noOfQuestionsAnswered = 0;
    public int correctAnswers = 0;
    public int wrongAnswers = 0;
    public bool isLevelFinished = false;
    //public static L1_GameManager gameManager;
    public int maxUsernameLength = 6;
    private string username;
    
    
    //Awake Method
    private void Awake()
    {
       
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        //

        errorText.gameObject.SetActive(false);
        //Set question on scene start...
        uIManager.SetQuestionElements(allQuestions[questionIndex]);
        questionCount = allQuestions.Length;
        noOfQuestions = allQuestions.Length;
        Debug.Log("No of Questions: " + noOfQuestions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnterUsername()
    {
        username = inputField.text;
        if (inputField.text.Length < maxUsernameLength)
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
               uIManager.SetUIActive(uIManager.endUI);
                runSendUsername();
           }
           else
           {
               if (usernameText)
                   usernameText.text = "@" + username;
               DisableInputUI();
               uIManager.SetUIActive(uIManager.proceedUI);
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
            uIManager.ShowAffirmationText("Correct!");
            uIManager.IncrementPointLocation();
            
            correctAnswers++;
            Debug.Log("Correct");
        }

        //If it is the wrong selection....
        else
        {
            uIManager.ShowAffirmationText("Incorrect!");
            wrongAnswers++;
            Debug.Log("Incorrect");
        }
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
            uIManager.SetQuestionElements(allQuestions[questionIndex]);
        }

        //Reset to first question index...
        else
        {
            OnLevelFinshed();
        }
    }

    public void OnLevelFinshed()
    {
        //isLevelFinished = true;
        uIManager.questionText.gameObject.SetActive(false);
        uIManager.FalseAnswerButton.gameObject.SetActive(false);
        uIManager.TrueAnswerButton.gameObject.SetActive(false);
        uIManager.questionTextBackground.gameObject.SetActive(false);
        qB_ContinueButton.gameObject.SetActive(true);
        //StartCoroutine(ShowResultsMenu(true));
    }

    public void SetLevelFinished(bool condition)
    {
        isLevelFinished = condition;
    }
    
}
