using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class L10_GameManager : MonoBehaviour
{
    //Main properties...
    [HideInInspector]
    private int score = 0;

    [SerializeField]
    private GameObject scoreScreen;

    [SerializeField]
    private TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void CheckEnemyPresent()
    {
        GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Existing Enemies: " + (existingEnemies.Length - 1));
        if (existingEnemies.Length - 1 <= 0)
        {
            // Do something...
            Invoke(nameof(ShowScoreScreen), 2.0f);
        }
    }

    public void AddToScore(int value)
    {
        score += value;
        Debug.Log("Score Points: " + score);
        CheckEnemyPresent();
    }

    private void ShowScoreScreen()
    {
        scoreScreen.SetActive(true);
        scoreText.text = "Score: " + score;
    }
}
