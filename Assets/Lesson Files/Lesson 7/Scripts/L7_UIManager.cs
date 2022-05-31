using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class L7_UIManager : MonoBehaviour
{
    //Properties...
    [Header("Main Properties")]
    [SerializeField]
    private TMP_Text coinText;
    [SerializeField]
    private RectTransform gameOverLayout;
    // Start is called before the first frame update
    void Start()
    {
        gameOverLayout.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCoinText(int value)
    {
        if(coinText)
        {
            coinText.text = "Coins: " + value.ToString();
        }
    }

    public void ClosePopupLayout(Button button)
    {
        button.transform.parent.parent.gameObject.SetActive(false);
        L7_GameManager.gameManager.IncreaseNoOfPopupsClosed();
    }

    public void ShowGameOverLayout()
    {
        if(gameOverLayout)
        {
            gameOverLayout.gameObject.SetActive(true);
        }
    }
}
