using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class L7_GameManager : MonoBehaviour
{
    //Properites
    [Header("Main Properties")]
    //Reference to the UI manager...
    [SerializeField]
    private L7_UIManager uiManager;
    //Array of all popup layout objects...
    [SerializeField]
    private RectTransform[] popupLayouts;
    public int popupLayoutIndex = 0;
    //Array/List of all imageObjects present in the scene...
    [SerializeField]
    private Image[] popupImages;
    //Reference to all popup scriptable objects associated with this scene...
    [SerializeField]
    private Popup[] allPopups;
    //Static variable...Total coins per click...
    public static int totalCoins = 0;
    //Variable that keeps track of popup layouts closed...
    public static int noOfPopupLayoutClosed = 0;
    private bool gameOver = false;
    [SerializeField]
    private int countdown = 45;
    [HideInInspector]
    public static L7_GameManager gameManager;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        //Check if allPopups array length is less than or equal to zero, If it is then do not proceed with this excecution...
        if (allPopups.Length <= 0)
            return;
        int i = 0;
        foreach(Image image in popupImages)
        {
            popupImages[i].sprite = allPopups[i].popupImage;
            Debug.Log(popupImages[i].name);
            i++;
        }
        uiManager.UpdateTimerText("00:" + countdown);
        //InvokeRepeating(nameof(Countdown), 2.0f, 1.0f);
        popupLayouts[popupLayoutIndex].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTotalCoins(int index)
    {
        //Check the popup scriptable object to see if the boolean isHarmful is set to true, if it is then get the required coins/points off the total coins/points...
        if (allPopups[index].isHarmful)
        {
            totalCoins -= allPopups[index].coins;
            if (totalCoins < 0)
                totalCoins = 0;
        }
        else
            totalCoins += allPopups[index].coins;

        popupImages[index].transform.parent.gameObject.SetActive(false);
        IncreaseNoOfPopupsClosed();

        if (uiManager)
            uiManager.SetCoinText(totalCoins);
        ///
        popupLayoutIndex++;
        DisplayNextPopupLayout(popupLayoutIndex);
    }

    public void DisplayNextPopupLayout(int index)
    {
        if(index < popupLayouts.Length)
            popupLayouts[index].gameObject.SetActive(true);
    }

    public void IncreaseNoOfPopupsClosed()
    {
        noOfPopupLayoutClosed++;
        Debug.Log("Number of Popups Closed: " + noOfPopupLayoutClosed);
        if(noOfPopupLayoutClosed == popupImages.Length)
        {
            gameOver = true;
        }
    }
    
    private void Countdown()
    {
        countdown--;
        if(countdown <= 0 || gameOver)
        {
            gameOver = true;
            foreach (Image image in popupImages)
            {
                image.transform.parent.gameObject.SetActive(false);
            }
            uiManager.ShowGameOverLayout();
            uiManager.UpdateTimerText("00:00");
            countdown = 0;
            CancelInvoke();
        }
        else
        {
            if (countdown >= 10)
                uiManager.UpdateTimerText("00:" + countdown);
            else
                uiManager.UpdateTimerText("00:0" + countdown);
        }
    }
}
