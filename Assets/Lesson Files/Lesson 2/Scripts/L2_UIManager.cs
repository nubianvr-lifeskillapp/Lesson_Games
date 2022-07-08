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
    [SerializeField]
    private TMP_Text affirmativeText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.gameObject.SetActive(false);
        affirmativeText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAffirmativeText()
    {
        if (!affirmativeText)
            return;

        affirmativeText.gameObject.SetActive(true);
        if (L2_GameManager.gameManager.totalPoints>= 90)
        {
            affirmativeText.text = "Excellent!!! Well done!";
        }
        else if(L2_GameManager.gameManager.totalPoints >=60 && L2_GameManager.gameManager.totalPoints < 90)
        {
            affirmativeText.text = "Good Job! But you can do better!";
        }
        else
        {
            affirmativeText.text = "Awwwn! This isn't looking so good";
        }
    }
    public void SetScoreText()
    {
        if(scoreText)
        {
            scoreText.text = "SCORE: " + L2_GameManager.gameManager.totalPoints;
            scoreText.gameObject.SetActive(true);

// #if UNITY_WEBGL == true && UNITY_EDITOR == false
//     printPoints ( L2_GameManager.gameManager.totalPoints);
// #endif
        }
        SetAffirmativeText();
    }
}
