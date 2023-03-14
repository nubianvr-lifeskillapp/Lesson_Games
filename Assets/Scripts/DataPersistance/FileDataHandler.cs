using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Fungus;
using UnityEngine.Scripting;

public class FileDataHandler
{
  private string dataDirPath;

  private string dataFileName;

  private string dataEncasingFolder;

  public FileDataHandler(string dataDirPath, string dataFileName, string dataEncasingFolder)
  {
    this.dataDirPath = dataDirPath;
    this.dataFileName = dataFileName + ".json";
    this.dataEncasingFolder = dataEncasingFolder;
  }

  public GameData Load(string fileName)
  {
    var fullPath = Path.Combine(dataDirPath, dataEncasingFolder, fileName + ".json");
    GameData loadedData = null;
    if (File.Exists(fullPath))
    {
      try
      {
        var dataToLoad = "";
        using (FileStream stream = new FileStream(fullPath, FileMode.Open))
        {
          using (StreamReader reader = new StreamReader(stream))
          {
            dataToLoad = reader.ReadToEnd();
          }
        }

        loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
      }
      catch (Exception e)
      {
        Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n"+ e);
      }
    }

    return loadedData;
  }

  public void Save(GameData data, string fileName)
  {
    var fullPath = Path.Combine(dataDirPath, dataEncasingFolder, fileName + ".json");
    {
      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        var dataToStore = JsonUtility.ToJson(data, true);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
          using (StreamWriter writer = new StreamWriter(stream))
          {
            writer.Write(dataToStore);
          }
        }
      }
      catch (Exception e)
      {
        Debug.LogError("Error occured when trying to save data to file: " + fullPath + "/n" + e);
      }
    }
  }
  
  public void SaveIncidentReport(IncidentReportingData data, string fileName)
  {
    var fullPath = Path.Combine(dataDirPath, dataEncasingFolder,fileName);
    {
      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        var dataToStore = JsonUtility.ToJson(data, true);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
          using (StreamWriter writer = new StreamWriter(stream))
          {
            writer.Write(dataToStore);
          }
        }
      }
      catch (Exception e)
      {
        Debug.LogError("Error occured when trying to save data to file: " + fullPath + "/n" + e);
      }
    }
  }
}
