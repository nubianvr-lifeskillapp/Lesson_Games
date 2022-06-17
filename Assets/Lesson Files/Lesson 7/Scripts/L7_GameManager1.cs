using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class L7_GameManager1 : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private L7_UIManager1 uImanager;
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
    public bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        RemoveAllPopups();
        popups[popupIndex].gameObject.SetActive(true);

        if (isGameOver == false)
        {
            countdownTimerText.text = "00:" + countdownTimer;
            InvokeRepeating(nameof(Countdown), 2.0f, 1.0f);
        }
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
        Debug.Log(totalCoins);
        //Update Coin Text...
        if(coinText)
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
            //Game Over...
            isGameOver = true;
            uImanager.gameplayUI.gameObject.SetActive(false);
            uImanager.gameOverUI.gameObject.SetActive(true);
            //Set Countdown Timer To Zero...
            countdownTimer = 0;
            //Display Session Stats/Results...
            popupIndex = 0;
        }
    }

    private void Countdown()
    {
        countdownTimer--;
        if(countdownTimer >= 0 && isGameOver == false)
        {
            //Update CountdownTimer Text...
            if (countdownTimerText)
            {
                if(countdownTimer>=10)
                    countdownTimerText.text = "00:" + countdownTimer;
                else
                    countdownTimerText.text = "00:0" + countdownTimer;
            }
        }
        else
        {
            //Game Over...
            isGameOver = true;
            //Stop All Invoke...
            CancelInvoke();
            Debug.Log("Cancel Invoke");
            //Set Countdown Timer To Zero...
            countdownTimer = 0;
            countdownTimerText.text = "00:00";
            //Remove All popups...
            RemoveAllPopups();
            //Display Session Stats/Results...
        }
    }
}
