using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverallGameManager : MonoBehaviour
{

    public static OverallGameManager overallGameManager;
    
    [DllImport("__Internal")]
    
    private static extern void sendLevelComplete(int sceneIndex);

    private static extern void loadLevel();

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
        
#if UNITY_WEBGL == true && UNITY_EDITOR == false
    loadLevel();
           
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Debug.Log ($"Loading Scene: {sceneIndex}");
    }
}
