using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncidentReportingManager : MonoBehaviour
{
    private string _incidentReportString;

    public TMP_InputField incidentReportInputField;

    private IncidentReportingData _incidentReportingData;

    private FileDataHandler _dataHandler;

    public GameObject errorText;

    private void Start()
    {
        errorText.SetActive(false);
    }

    private string CreateFileDirectoryName()
    {
        var fileDirectory = "IncidentReports/"+DataPersistanceManager.instance.GetGameData().username +" "+ DateTime.Now.ToString("dddd, dd MMMM yyyy HH.mm.ss") + ".json";
        return fileDirectory;
    }
    

    public void ReportIncident()
    {
        if (incidentReportInputField.text.Length <= 0)
        {
            errorText.SetActive(true);  
        }
        else
        {
            _incidentReportString = incidentReportInputField.text;
                     
            _dataHandler = new FileDataHandler(Application.persistentDataPath, CreateFileDirectoryName(), OverallGameManager.overallGameManager.playerData.schoolName);
                   
            _incidentReportingData = new IncidentReportingData(DataPersistanceManager.instance.GetGameData().username,_incidentReportString,DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
                    
            _dataHandler.SaveIncidentReport(_incidentReportingData,CreateFileDirectoryName());
                    
            ClearInputBox();
        }

        
    }

    public void ClearInputBox()
    {
        incidentReportInputField.text = "";
    }
}
