using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IncidentReportingData
{
    public string username;
    public string reportedIncident;
    public string date;

    public IncidentReportingData(string username, string reportedIncident, string date)
    {
        this.username = username;
        this.reportedIncident = reportedIncident;
        this.date = date;
    }
}
