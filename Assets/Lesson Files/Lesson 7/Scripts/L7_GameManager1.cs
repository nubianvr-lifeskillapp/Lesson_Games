using System.Collections;
using System.Collections.Generic;
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
    private int countdownTimer = 10;
    [SerializeField]
    private TMP_Text countdownTimerText;
    [SerializeField]
    private TMP_Text coinText;
    [SerializeField]
    private TMP_Text scoreText;
    private int roundCount = 0;
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
        RemoveAllPopups();
        countdownTimerText.text = "00:" + countdownTimer;
        //InvokeRepeating(nameof(Countdown), 3.0f, 1.0f);
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
        popups[popupIndex].gameObject.SetActive(false);
        popupIndex++;
        if (popupIndex < popups.Length)
        {
            popups[popupIndex].gameObject.SetActive(true);
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

    private void Countdown()
    {
        countdownTimer--;
        //Debug.Log("Countdown: " + countdownTimer);
        if (countdownTimer >= 0)
        {
            //Update CountdownTimer Text...
            if (countdownTimerText)
            {
                if (countdownTimer >= 10)
                    countdownTimerText.text = "00:" + countdownTimer;
                else
                    countdownTimerText.text = "00:0" + countdownTimer;
            }
        }
        else
        {
            //Stop All Invoke...
            CancelInvoke();
            Debug.Log("Cancel Invoke");
            //Set Countdown Timer To Zero...
            countdownTimer = 0;
            countdownTimerText.text = "00:00";
            //Remove All popups...
            RemoveAllPopups();
            // Call finish gameplay function...
            FinishedGameplay();
        }
    }

    private void FinishedGameplay()
    {
        // Set scoreText...
        scoreText.text = "Coins: " + totalCoins;
        // Show continue button...
        gameplayContinueButton.SetActive(true);
        //ShowScreen(gameOverScreen);
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
    private void Reset()
    {
        //Show Popup...
        countdownTimer = 10;
        popupIndex = 0;
        totalCoins = 0;
        popups[popupIndex].gameObject.SetActive(true);
        countdownTimerText.text = "00:" + countdownTimer;
    }
    public void Intro()
    {
        switch (roundCount)
        {
            case 0:
                Reset();
                gameplayContinueButton.SetActive(false);
                InvokeRepeating(nameof(Countdown), 3.0f, 1.0f);
                ShowScreen(gameplayScreen);
                break;
            case 1:
                Reset();
                gameplayContinueButton.SetActive(false);
                InvokeRepeating(nameof(Countdown), 3.0f, 1.0f);
                ShowScreen(gameplayScreen);
                break;
            case 2:
                ShowScreen(endScreen);
                break;
        }
    }

    public void Over()
    {
        roundCount++;
        Debug.Log("Round Count: " + roundCount);
        switch (roundCount)
        {
            case 0:
                ShowScreen(videoScreen);
                break;
            case 1:
                ShowScreen(videoScreen);
                break;
            case 2:
                ShowScreen(introScreen);
                break;
        }
    }
}
