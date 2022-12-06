using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string username;
    public int currentLesson;
    public string className;
    public string educationalLevel;
    public string sex;
    public bool firstTime;

    public GameData()
    {
        username = "";
        currentLesson = 0;
        className = "";
        educationalLevel = "";
        sex = "";
        firstTime = true;
    }

}
