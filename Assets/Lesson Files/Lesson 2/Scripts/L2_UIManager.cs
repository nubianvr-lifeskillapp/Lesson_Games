using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class L2_UIManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void printPoints(int score);
    [SerializeField]
    private TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetScoreText()
    {
        if(scoreText)
        {
            scoreText.text = "Score: " + GameManager.gameManager.totalPoints;
            scoreText.gameObject.SetActive(true);

#if UNITY_WEBGL == true && UNITY_EDITOR == false
    printPoints ( GameManager.gameManager.totalPoints);
#endif
        }
    }
}
