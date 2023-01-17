using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class TriviaSystem : MonoBehaviour
{
    //Array to hold all trivia question object...
    [SerializeField]
    private TriviaQuestionObject[] questionObjects;

    private int noOfQuestions;
    private int noOfCorrectAnswers;
    private int noOfWrongAnswers;

    // A variable to keep track of the question index...
    private int questionIndex = 0;
    [SerializeField]
    private GameObject questionBoxUI;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private TMP_Text questionNumberText;
    // Array holding reference to all buttons on the trivia layout...
    [SerializeField]
    private TriviaButtons[] answerBtns;
    

    // Array holding reference to question text ui...
    [SerializeField] TMP_Text questionText;
    [SerializeField]
    private TMP_Text commentaryText;
    [SerializeField]
    private TMP_Text endCommentaryText;
    [SerializeField]
    private TMP_Text priceText;
    [SerializeField]
    private TMP_Text detailsText;

    public TMP_Text endScreenBtnText;

    private int price = 0;



    [SerializeField]
    private TMP_Text resultPriceText;
    [SerializeField]
    private TMP_Text timerText;
    [SerializeField]
    private Sprite defaultSprite;

    [SerializeField]
    private Sprite correctSprite;
    [SerializeField]
    private Sprite wrongSprite;
    private TriviaButtons selectedButton;

    public float timer = 30;
    public bool timerIsRunning;
    public Flowchart Flowchart;



    // Start is called before the first frame update
    void Start()
    {
        PlaySfx("BackgroundMusic");
        gameOverUI.SetActive(false);
        OverallGameManager.overallGameManager.playerData.currentLesson = SceneManager.GetActiveScene().buildIndex;
        noOfQuestions = questionObjects.Length;
        SetSelectedButton(answerBtns[0]);
       
    }

    // Update is called once per frame
    void Update()
    {
        timerIsRunning = Flowchart.GetBooleanVariable("timerRunning");
        if (!timerIsRunning) return;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            DisplayTime(timer);
        }
        else
        {
            Debug.Log("Time has run out!");
            timer = 0;
            PlaySfx("LoseSound");
            Flowchart.SetBooleanVariable("timerRunning", false);
            timerIsRunning = false;
            SetButtonsInteractable(false);
            Invoke("ShowGameOverUI",2.0f);
        }
    }
    
    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void SetUIElements()
    {
        timer = 30;
        questionNumberText.text = (questionIndex + 1) + "/" + questionObjects.Length;
        questionBoxUI.SetActive(true);
        commentaryText.gameObject.SetActive(false);
        questionText.text = questionObjects[questionIndex].question;
        // Set answer text elements...
        for (int i = 0; i < answerBtns.Length; i++)
        {
            answerBtns[i].SetButtonActive(false);
        }
        for (int i = 0; i < questionObjects[questionIndex].answers.Length; i++)
        {
            answerBtns[i].SetButtonActive(true);
            answerBtns[i].textComponent.text = questionObjects[questionIndex].answers[i];
        }
        detailsText.text = "This question is worth $" + questionObjects[questionIndex].points;
        //Set button image color to white...
        //selectedButton.GetComponent<Image>().color = Color.white;
        selectedButton.btnImage.sprite = defaultSprite;
        SetButtonsInteractable(true);
    }

    public void CheckSelection(int index)
    {
        SetButtonsInteractable(false);
        if (!commentaryText)
            return;
        commentaryText.gameObject.SetActive(true);
        if (index == questionObjects[questionIndex].correctAnswerIndex)
        {
            Debug.Log("Correct Answer!!!");
            commentaryText.text = "Correct!";
            commentaryText.color = Color.green;
            noOfCorrectAnswers++;
            PlaySfx("PowerUp");
            price = questionObjects[questionIndex].points;
            priceText.text = "$" + price;
            //Set button image color to green...
            //selectedButton.GetComponent<Image>().color = Color.green;
            selectedButton.btnImage.sprite = correctSprite;
            //MoveToNextQuestion...
            if (questionIndex < questionObjects.Length - 1)
            {
                questionIndex++;
                Invoke(nameof(SetUIElements), 2.0f);
            }
            else
            {
                Invoke(nameof(ShowGameOverUI), 2.0f);
            }
        }
        else
        {
            Debug.Log("Incorrect Answer!!!");
            PlaySfx("LoseSound");
            selectedButton.btnImage.color = Color.red;
            selectedButton.btnImage.sprite = wrongSprite;
            Flowchart.SetBooleanVariable("timerRunning", false);
            Invoke(nameof(ShowGameOverUI), 2.0f);
            
            commentaryText.text = "Incorrect!";
            commentaryText.color = Color.red;
            noOfWrongAnswers++;
        }
    }
    private void ShowGameOverUI()
    {
        if (price <= 0)
            endCommentaryText.text = "Awnn, try again!!!";
        else if (price > 0 && price <= 5000)
            endCommentaryText.text = "You can do better!!!";
        else if (price > 5000 && price <= 10000)
            endCommentaryText.text = "Good work!!!";
        else if (price > 10000 && price <= 20000)
            endCommentaryText.text = "Great job!!!";
        
        else
            endCommentaryText.text = "Excellent Performance!!!";

        resultPriceText.text = priceText.text;
        questionBoxUI.SetActive(false);
        gameOverUI.SetActive(true);
        SetButtonTextOnScreen();
    }
    public void SetSelectedButton(TriviaButtons button)
    {
        selectedButton = button;
    }

    public void SetButtonTextOnScreen()
    {
        endScreenBtnText.text = price >= 10000 ? "Continue" : "Retry";
    }

    public void OnEndButtonPressed()
    {
        if (price >= 10000)
        {
            LoadNextScene(14);
        }
        else
        {
            ResetScene();
        }
    }

    private void SetButtonsInteractable(bool condition)
    {
        for (int i = 0; i < answerBtns.Length; i++)
        {
            answerBtns[i].btn.interactable = condition;
        }
    }

    public void PlaySfx(string soundName)
    {
        SoundManager.soundManager.PlaySFX(soundName);
    }

    private void StopAllSfx()
    {
        SoundManager.soundManager.StopAllSFX();
    }

    public void LoadNextScene(int buildIndex)
    {
        OverallGameManager.overallGameManager.LoadNextScene(buildIndex);
    }

    public void ResetScene()
    {
        OverallGameManager.overallGameManager.ReloadScene();
    }


}
