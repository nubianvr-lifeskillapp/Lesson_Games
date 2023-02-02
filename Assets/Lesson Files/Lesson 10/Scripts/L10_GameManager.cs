using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class L10_GameManager : MonoBehaviour
{
    //Main properties...
    [HideInInspector]
    private int score = 0;

    [SerializeField]
    private GameObject scoreScreen;

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject introScreen;
    [SerializeField]
    private GameObject gameplayScreen;
    [SerializeField]
    private GameObject timerScreen;
    [SerializeField]
    private GameObject endScreen;

    public Flowchart Flowchart;
    
    public bool timerIsRunning;
    
    private float timeValue;

    private float prevTime;

    private bool isFirstPlay;
    
    public TMP_Text timeText;
    public TMP_Text finalTimeText;
    
    // Start is called before the first frame update
    void Start()
    {
        PlaySfx("BackgroundMusic");
        OverallGameManager.overallGameManager.playerData.currentLesson = SceneManager.GetActiveScene().buildIndex;
        //ShowScreen(introScreen);
        //scoreScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TimerMethod();
    }
    
    public void TimerMethod()
    {
        
        if (!timerIsRunning) return;
        timeValue += Time.deltaTime;
        DisplayTime(timeValue, timeText);
    }
    
    void DisplayTime(float timeToDisplay, TMP_Text textBox)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        textBox.text = string.Format(" {0:00}:{1:00}", minutes, seconds);
    }
    
    public void setTimerToFalse()
    {
        timerIsRunning = false;
    }

    public void setTimerToTrue()
    {
        timerIsRunning = true;
    }

    public void ResetTimer()
    {
        timeValue = 0;
    }

    private void CheckEnemyPresent()
    {
        GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Existing Enemies: " + (existingEnemies.Length - 1));
        if (existingEnemies.Length - 1 <= 0)
        {
            timerIsRunning = false;
            timerScreen.SetActive(timerIsRunning);
            // Do something...
            //Invoke(nameof(ShowScoreScreen), 2.0f);
            Flowchart.ExecuteBlock("ExecuteAfterGameComplete");
            DisplayTime(timeValue, finalTimeText);
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
        //scoreText.text = "Score: " + score;
        
    }


    public void ShowScreen(GameObject screen)
    {
        introScreen.SetActive(false);
        gameplayScreen.SetActive(false);
        timerScreen.SetActive(false);
        endScreen.SetActive(false);

        screen.SetActive(true);
    }

    public void ExecuteAfterVideo()
    {
        Flowchart.ExecuteBlock("AfterVideo");
    }

    public void PlaySfx(string soundName)
    {
        SoundManager.soundManager.PlaySFX(soundName);
    }

    public void StopAllSfx()
    {
        SoundManager.soundManager.StopAllSFX();
    }

    public void LoadNextScene(int sceneNumber)
    {
        if (sceneNumber != 13) return;
        OverallGameManager.overallGameManager.LoadNextScene(
            OverallGameManager.overallGameManager.playerData.postTestDone ? 11 : sceneNumber);
    }

    public void ReloadScene()
    {
        OverallGameManager.overallGameManager.ReloadScene();
    }
}

