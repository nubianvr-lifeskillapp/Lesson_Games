using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;
using DG.Tweening;
using NubianVR.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OfflineLoginManager : MonoBehaviour, IDataPersistance
{
    [Header("UI Components")]
    public TMP_InputField usernameTextBox;
    public TMP_InputField passwordTextBox;
    public TMP_Text errorText;
    public CanvasGroup panelOverlay;
    
    //User Game Data 

    private string _sex;
  
    private string _educationalLevel;
    
    private string _className;
  
    private bool _firstTime = true;
    
    private int _currentLesson;
    
    private string _username;

    private string uniquePCID;

    private string pcName;
    
    [HideInInspector]
    public string _schoolName;
    
    string result;

    private string loginPath;

    private bool _preTestDone;

    private string _userNamePrefix = "lsa-";
    
    [Header("UI Screens")]
    public UI_System uiManager;
    public UI_Screen selectGenderScreen;
    public UI_Screen selectEducationalScreen;
    public UI_Screen menuScreen;
    public UI_Screen selectPrimaryClass;
    public UI_Screen selectJHSClass;
    public UI_Screen selectSHSClass;
    public UI_Screen preTestScreen;


    // Start is called before the first frame update
    void Start()
    {
        errorText.gameObject.SetActive(false);
        panelOverlay.gameObject.SetActive(false);
        panelOverlay.DOFade(0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VerifyUser()
    {

        if ((usernameTextBox.text.Length > 0))
        {
            var tempUsername = usernameTextBox.text;
            var username = _userNamePrefix + tempUsername.Replace(" ", string.Empty);
            var tempPassword = passwordTextBox.text;
            var password = tempPassword.Replace(" ", string.Empty);

            var path = Path.Combine(Application.streamingAssetsPath, $"login-{_schoolName}.txt");
            
            var reader = new WWW(path);

            while (!reader.isDone)
            {
            }
            var realPath = Application.persistentDataPath + "/login";
            File.WriteAllBytes(realPath, reader.bytes);
  
            var ObjPath = realPath;

            string[] lines = File.ReadAllLines(ObjPath);
            
            for (var i = 0; i < lines.Length; i++)
            {
                var field = lines[i].Split(',');
                if (field[0].Equals(username) && field[1].Equals(password))
                {
                    panelOverlay.gameObject.SetActive(true);
                    panelOverlay.DOFade(1, 0.1f);
                    DataPersistanceManager.instance.playerLogin(username, _schoolName);
                    _username = username;
                    OverallGameManager.overallGameManager.playerData.username = username;
                    print(_username);
                    return;
                }
            }
            errorText.text = "Username or Password is incorrect";
            errorText.gameObject.SetActive(true);
        }
        else
        {
            errorText.text = "Please enter your Username and Password";
            errorText.gameObject.SetActive(true);
        }
        
    }

    public void LoadData(GameData data)
    {
        OverallGameManager.overallGameManager.playerData = data;
        OverallGameManager.overallGameManager.playerData.username = _username;
        OverallGameManager.overallGameManager.playerData.schoolName = _schoolName;
        DataPersistanceManager.instance.SetGameData(data);
        _sex = data.sex;
        _educationalLevel = data.educationalLevel;
        _className = data.className;
        _firstTime = data.firstTime;
        _currentLesson = data.currentLesson;
        _schoolName = data.schoolName;
        //_username = data.username;
        _preTestDone = data.preTestDone;
        uniquePCID = SystemInfo.deviceUniqueIdentifier;
        pcName = SystemInfo.deviceName;
        ChangeScreen(data.firstTime ? selectGenderScreen : menuScreen);
        // Debug.Log("Offline Login Manager: Sex " +_sex + " " + DateTime.Now);
        // Debug.Log("Offline Login Manager: Education Level " +_educationalLevel + " " + DateTime.Now);
        // Debug.Log("Offline Login Manager: Current Lesson " +_currentLesson + " " + DateTime.Now);
        // Debug.Log("Offline Login Manager: Username " + OverallGameManager.overallGameManager.playerData.username + " " + DateTime.Now);


    }

    public void SaveData(GameData data)
    {
        data.username = _username;
        data.sex = _sex;
        data.educationalLevel = _educationalLevel;
        data.className = _className;
        data.firstTime = _firstTime;
        data.schoolName = _schoolName;
        //data.uniquePCID = uniquePCID;
        //data.pcName = pcName;

        //data.currentLesson = _currentLesson;
    }

    private void ChangeScreen(UI_Screen screen)
    {
        uiManager.SwitchScreens(screen);
        DataPersistanceManager.instance.SaveStudentGame();
    }

    public void SelectStudentSex(SelectSexBtn studentSex)
    {
        _sex = studentSex.studentSex switch
        {
            sex.Male => "male",
            sex.Female => "female",
            _ => ""
        };
    }

    public void SelectStudentEducationLevel(SelectEducationLevelBtn level)
    {
        switch (level.studentEducationLevel)
        {
            case educationalLevel.Primary:
                _educationalLevel = "primary";
                ChangeScreen(selectPrimaryClass);
                break;
            case educationalLevel.JuniorHigh:
                _educationalLevel = "juniorhigh";
                ChangeScreen(selectJHSClass);
                break;
            case educationalLevel.SeniorHigh:
                _educationalLevel = "seniorhigh";
                ChangeScreen(selectSHSClass);
                break;
            default:
                break;
        }
    }

    public void SelectPrimaryClass(SelectPrimaryClassBtn primaryClassBtn)
    {
        switch (primaryClassBtn.PrimaryClass)
        {
            case PrimaryClasses.Class4:
                _className = "Class 4";
                break;
            case PrimaryClasses.Class5:
                _className = "Class 5";
                break;
            case PrimaryClasses.Class6:
                _className = "Class 6";
                break;
            default:
                break;
        }

        _firstTime = false;
        ChangeScreen(menuScreen);
    }

    public void SelectJhsClass(SelectJhsClassBtn jhsClassBtn)
    {
        switch (jhsClassBtn.JuniorHighClass)
        {
            case JuniorHighClasses.Jhs1:
                _className = "JHS 1";
                break;
            case JuniorHighClasses.Jhs2:
                _className = "JHS 2";
                break;
            case JuniorHighClasses.Jhs3:
                _className = "JHS 3";
                break;
            default:
                break;
        }
        _firstTime = false;
        ChangeScreen(menuScreen);
    }

    public void SelectShsClassMethod(SelectShsClassBtn shsClassBtn)
    {
        switch (shsClassBtn.SeniorHighClass)
        {
            case SeniorHighClasses.Shs1:
                _className = "SHS 1";
                break;
            case SeniorHighClasses.Shs2:
                _className = "SHS 2";
                break;
            case SeniorHighClasses.Shs3:
                _className = "SHS 3";
                break;
            default:
                break;
        }
        _firstTime = false;
        ChangeScreen(menuScreen);
    }

    public void StartGameBtn()
    {
        if (_preTestDone)
        {
            OverallGameManager.overallGameManager.LoadNextScene(_currentLesson == 0 ? 1: _currentLesson);
        }
        else
        {
            OverallGameManager.overallGameManager.LoadNextScene(_currentLesson > 0 ? _currentLesson : 13);
        }

        
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
