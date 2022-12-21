using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class L7_GameManager1 : MonoBehaviour
{
    //Properties...
    private int totalCoins;
    [SerializeField]
    private RectTransform[] popups;
    private int popupIndex = 0;
    [SerializeField]
    [Tooltip("Value for duration of timer")]
    private float countdownTimer = 10;

    private bool timerIsRunning;
    [SerializeField]
    private TMP_Text countdownTimerText;
    [SerializeField]
    private TMP_Text coinText;
    [SerializeField]
    private TMP_Text scoreText;
    private int roundCount = 0;
    [SerializeField]
    private GameObject gameplayScreen;
    [SerializeField]
    private GameObject gameplayContinueButton;
    [SerializeField]
    private GameObject gameOverScreen;

    private List<RectTransform> popUpsList;
    private int noOfQuestionsAnswered;


    public Flowchart Flowchart;

    // Start is called before the first frame update
    void Start()
    {
        PlaySfx("BackgroundMusic");
        timerIsRunning = false;
        noOfQuestionsAnswered = 0;
        RemoveAllPopups();
        popUpsList = popups.ToList();
        popupIndex = Random.Range(0, popUpsList.Count);
        popUpsList[popupIndex].gameObject.SetActive(true);
    }

    private void RemoveAllPopups()
    {
        foreach (RectTransform popup in popups)
        {
            popup.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerIsRunning) return;
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            DisplayTime(countdownTimer);
        }
        else
        {
            Debug.Log("Time has run out!");
            countdownTimer = 0;
            timerIsRunning = false;
            FinishedGameplay();
        }
    }

    public void SetTotalCoins(int coins)
    {
        totalCoins += coins;
        //Debug.Log(totalCoins);
        //Update Coin Text...
        if (coinText)
            coinText.text = totalCoins.ToString();
        MoveToNextPopup();
    }
    private void MoveToNextPopup()
    {
        noOfQuestionsAnswered++;
        popUpsList[popupIndex].gameObject.SetActive(false);
        popUpsList.Remove(popUpsList[popupIndex]);
        popupIndex = Random.Range(0, popUpsList.Count);
        
        if (noOfQuestionsAnswered != popups.Length)
        {
            popUpsList[popupIndex].gameObject.SetActive(true);
        }
        else
        {
            // Call finish gameplay function...
            FinishedGameplay();
            //Set Countdown Timer To Zero...
            countdownTimer = 0;
            //Display Session Stats/Results...
            popupIndex = 0;
        }
    }
    

    private void FinishedGameplay()
    {
        // Set scoreText...
        //scoreText.text = "Coins: " + totalCoins;
        // Show continue button...
        //gameplayContinueButton.SetActive(true);
        //ShowScreen(gameOverScreen);
        if (totalCoins > 30)
        {
            Flowchart.ExecuteBlock("CoinsMoreThan30");
        }
        else
        {
            Flowchart.ExecuteBlock("CoinsLessThan30");
        }
    }
   
    private void Reset()
    {
        //Show Popup...
        RemoveAllPopups();
        countdownTimer = 30;
        popupIndex = 0;
        totalCoins = 0;
        popUpsList = popups.ToList();
        popupIndex = Random.Range(0, popUpsList.Count);
        popUpsList[popupIndex].gameObject.SetActive(true);
        
    }
    
    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        countdownTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ExecuteAfterVideo()
    {
        Flowchart.ExecuteBlock("AfterVideo");
    }

    public void PlaySfx(string soundName)
    {
        SoundManager.soundManager.PlaySFX(soundName);
    }

    public void StopAllSFX()
    {
        SoundManager.soundManager.StopAllSFX();
    }

    public void ReloadScene()
    {
        OverallGameManager.overallGameManager.ReloadScene();
    }

    public void SetTimerRunningTrue()
    {
        timerIsRunning = true;
    }

    public void LoadNextScene(int sceneIndex)
    {
        OverallGameManager.overallGameManager.LoadNextScene(sceneIndex);
    }
    
}
