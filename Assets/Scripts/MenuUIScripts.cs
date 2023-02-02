using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuUIScripts : MonoBehaviour, IDataPersistance
{
    public TMP_Text username;
    public TMP_Text schoolText;
    public TMP_Text classText;
    public TMP_Text currentLessonText;
    public OfflineLoginManager LoginManager;

    private void SetUserData(GameData data)
    {
        //username.text = "Welcome " + data.username;
        classText.text = data.className;
        Debug.Log("Menu UI Scripts: " + data.className);
        currentLessonText.text = data.currentLesson.ToString();
        schoolText.text = data.schoolName;
    }

    public void LoadData(GameData data)
    {
        SetUserData(data);
    }

    public void SaveData(GameData data)
    {
        //SetUserData(data);
    }

    public void LoadGameData()
    {
        username.text = "Welcome " + DataPersistanceManager.instance.GetGameData().username;
        classText.text = DataPersistanceManager.instance.GetGameData().className;
        currentLessonText.text = DataPersistanceManager.instance.GetGameData().currentLesson.ToString();
        schoolText.text = DataPersistanceManager.instance.GetGameData().schoolName;
        Debug.Log("Menu UI Scripts Username: " + DataPersistanceManager.instance.GetGameData().username + " " + DateTime.Now);
        Debug.Log("Menu UI Scripts Classname: " + DataPersistanceManager.instance.GetGameData().className + " " + DateTime.Now);
        Debug.Log("Menu UI Scripts Current Lesson: " + DataPersistanceManager.instance.GetGameData().currentLesson + " " + DateTime.Now);
        Debug.Log("Menu UI Scripts School Name: " + DataPersistanceManager.instance.GetGameData().schoolName + " " + DateTime.Now);
    }
}
