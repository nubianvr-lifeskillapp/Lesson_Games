using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{

    private string fileName;
    private GameData gameData { get; set; }
    
    

    private List<IDataPersistance> _dataPersistancesObjects;

    private FileDataHandler _dataHandler;
    public static DataPersistanceManager instance { get; private set; }

    public GameData GetGameData()
    {
        return gameData;
    }

    public void SetGameData(GameData data)
    {
        gameData = data;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("More than one Data Persistence Manager Found");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
       //playerLogin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerLogin(string playerFileName, string playerSchoolName)
    {
        fileName = playerFileName;
        _dataHandler = new FileDataHandler(Application.persistentDataPath, playerFileName, playerSchoolName);
        _dataPersistancesObjects = FindAllDataPersistanceObjects();
        
        LoadStudentGame();
    }

    public void NewStudentGame()
    {
        gameData = new GameData();
        
    }

    public void LoadStudentGame()
    {
        gameData = _dataHandler.Load(fileName);
        if (gameData == null)
        {
          
            NewStudentGame();
        }

        foreach (var dataPersistancesObj in _dataPersistancesObjects)
        {
            dataPersistancesObj.LoadData(gameData);
        }
    }

    public void SaveStudentGame()
    {
        foreach (var dataPersistancesObj in _dataPersistancesObjects)
        {
            dataPersistancesObj.SaveData(gameData);
        }
        _dataHandler.Save(gameData, fileName);
    }

    private void OnApplicationQuit()
    {
        SaveStudentGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
