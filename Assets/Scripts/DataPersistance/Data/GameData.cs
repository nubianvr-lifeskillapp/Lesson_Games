using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string username;
    public int currentLesson;
    public int currentPlayThroughNumber;
    public string schoolName;
    public string className;
    public string educationalLevel;
    public string sex;
    public bool firstTime;
    public bool preTestDone;
    public bool postTestDone;

    public GameData()
    {
        username = "";
        currentLesson = 0;
        className = "";
        educationalLevel = "";
        //School name default value changes according to the build being made
        schoolName = "Nubian Academy";
        sex = "";
        firstTime = true;
        preTestDone = false;
        postTestDone = false;
        currentPlayThroughNumber = 1;
    }

}
