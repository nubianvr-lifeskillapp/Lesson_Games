using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class L1_GameManager : MonoBehaviour
{
    //Properties...
  
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private TMP_Text errorText;
    [SerializeField]
    private L1_UIManager uIManager;

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
    public static L1_GameManager gameManager;
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
        //
        uIManager.ShowInputUI(true);
        uIManager.ShowQuestionUI(false, 0.0f);
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
        if(inputField.text.Length < 4)
        {
            errorText.gameObject.SetActive(true);
            //Debug.Log("Input text not up to 6 characters...");
        }
        else
        {
            DisableInputUI();
        }
    }

    private void DisableInputUI()
    {
        //Disable input UI...
        uIManager.ShowInputUI(false);
        //Show questions UI...
        uIManager.ShowQuestionUI(true, 0.0f);
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
