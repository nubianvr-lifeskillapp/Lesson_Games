using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverallGameManager : MonoBehaviour, IDataPersistance
{

    public static OverallGameManager overallGameManager;
    
    [DllImport("__Internal")]
    private static extern void sendLevelComplete(int sceneIndex);
    
    [DllImport("__Internal")]
    private static extern void loadLevel();

    public GameData playerData;

    private void Awake()
    {
        if (overallGameManager == null)
        {
            overallGameManager = this;
            DontDestroyOnLoad(this);
            
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetSceneToLoad()
    {
#if UNITY_WEBGL == true && UNITY_EDITOR == false
    loadLevel();
    Debug.Log ("Level Load Sent");
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadNextScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
#if UNITY_WEBGL == true && UNITY_EDITOR == false
    sendLevelComplete(buildIndex);
           
#endif
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateCurrentLessonProgress()
    {
        playerData.currentLesson = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Debug.Log ($"Loading Scene: {sceneIndex}");
    }

    public void LoadData(GameData data)
    {
         // playerData.sex = data.sex;
         // playerData.username = data.username;
         // playerData.className = data.className;
         // playerData.currentLesson = data.currentLesson;
         // playerData.educationalLevel = data.educationalLevel;
         // playerData.firstTime = data.firstTime;
         // playerData.postTestDone = data.postTestDone;
         // playerData.preTestDone = data.preTestDone;
         // playerData.currentPlayThroughNumber = data.currentPlayThroughNumber;
         
         Debug.Log("Overall Game Manager: Sex " + data.sex + " " + DateTime.Now);
         Debug.Log("Overall Game Manager: Education Level " + data.educationalLevel + " " + DateTime.Now);
         Debug.Log("Overall Game Manager: Current Lesson " + data.currentLesson + " " + DateTime.Now);
    }

    public void SaveData(GameData data)
    {
        data.sex = playerData.sex;
        data.username = playerData.username;
        data.className = playerData.className;
        data.currentLesson = playerData.currentLesson;
        data.educationalLevel = playerData.educationalLevel;
        data.firstTime = playerData.firstTime;
        data.postTestDone = playerData.postTestDone;
        data.preTestDone = playerData.preTestDone;
        data.currentPlayThroughNumber = playerData.currentPlayThroughNumber;
    }
}
