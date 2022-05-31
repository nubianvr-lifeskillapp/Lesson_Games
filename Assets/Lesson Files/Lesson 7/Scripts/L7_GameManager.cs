using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L7_GameManager : MonoBehaviour
{
    //Properites
    [Header("Main Properties")]
    //Reference to the UI manager...
    [SerializeField]
    private L7_UIManager uiManager;
    //Array/List of all imageObjects present in the scene...
    [SerializeField]
    private Image[] imageObjects;
    //Reference to all popup scriptable objects associated with this scene...
    [SerializeField]
    private Popup[] allPopups;
    //Static variable...Total coins per click...
    public static int totalCoins = 0;
    //Variable that keeps track of popup layouts closed...
    public static int noOfPopupLayoutClosed = 0;

    private bool gameOver = false;


    [HideInInspector]
    public static L7_GameManager gameManager;

    private void Awake()
    {
        if (gameManager == null || gameManager != this)
        {
            gameManager = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Check if allPopups array length is less than or equal to zero, If it is then do not proceed with this excecution...
        if (allPopups.Length <= 0)
            return;


        int i = 0;
        foreach(Image image in imageObjects)
        {
            imageObjects[i].sprite = allPopups[i].popupImage;
            Debug.Log(imageObjects[i].name);
            i++;
        }
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

        imageObjects[index].transform.parent.gameObject.SetActive(false);
        IncreaseNoOfPopupsClosed();

        if (uiManager)
            uiManager.SetCoinText(totalCoins);
    }

    public void IncreaseNoOfPopupsClosed()
    {
        noOfPopupLayoutClosed++;
        if(noOfPopupLayoutClosed == imageObjects.Length)
        {
            gameOver = true;
            Debug.Log("GameOver!");
            uiManager.ShowGameOverLayout();
        }
    }
}
