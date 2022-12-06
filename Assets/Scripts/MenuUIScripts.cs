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

    private void SetUserData(GameData data)
    {
        username.text = "Welcome " + data.username;
        classText.text = data.className;
        currentLessonText.text = data.currentLesson.ToString();
    }

    public void LoadData(GameData data)
    {
        SetUserData(data);
    }

    public void SaveData(GameData data)
    {
        SetUserData(data);
    }
}
