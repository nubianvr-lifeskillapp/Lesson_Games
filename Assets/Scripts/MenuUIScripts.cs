using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using TMPro;
using UnityEngine;

public class MenuUIScripts : MonoBehaviour
{
    public TMP_Text username;
    public TMP_Text schoolText;
    public TMP_Text classText;
    public TMP_Text currentLessonText;
    public OfflineLoginManager LoginManager;

    public void SetUserData()
    {
        username.text = "Welcome " + OverallGameManager.overallGameManager.playerData.username;
        classText.text = OverallGameManager.overallGameManager.playerData.className;
        //Debug.Log("Menu UI Scripts: " + data.className);
        schoolText.text = OverallGameManager.overallGameManager.playerData.schoolName;
        currentLessonText.text = OverallGameManager.overallGameManager.playerData.currentLesson == 0 ? 0.ToString() : (OverallGameManager.overallGameManager.playerData.currentLesson-1).ToString();
    }
    
}
