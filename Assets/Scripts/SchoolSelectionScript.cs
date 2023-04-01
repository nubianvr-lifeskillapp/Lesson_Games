using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SchoolSelectionScript : MonoBehaviour
{

    private List<string>schoolNamesList = new List<string>(){
        "Please select a school",
        "Nubian Admin",
        "Queen of Peace Basic School",
        "Sowa Din Basic School",
        "Nii Kojo Ababio Basic School",
        "L&A Memorial Basic School",
        "Field Engineer Basic School",
        "Odorgonno Model '1' Basic School",
        "St Joseph's Basic School",
        "Kaneshie Kingsway Junior High School",
        "St Peter's Basic School",
        "Naval Base Basic School", 
        "Abokobi Presby '1' Basic School",
        "Ebenezer Senior High School",
        "Osu Presby Basic School",
        "Kinbu Senior High School",
        "Accra Senior High School",
        "Frafra Senior High School",
        "Wesley Grammar Senior High School",
        "Kwabenya Senior High School",
        "Labone Senior High School",
        "Kaneshie Senior High School",
        "Achimota Senior High School",
        ""
    };

    public TMP_Dropdown dropDown;
    public OfflineLoginManager loginManager;
    public GameObject goBtn;
    

    // Start is called before the first frame update
    void Start()
    {
        PopulateList();
        
        dropDown.onValueChanged.AddListener(delegate { SchoolValueNameChanged(dropDown); });
        
        goBtn.SetActive(false);
        
        print(schoolNamesList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PopulateList()
    {
        dropDown.AddOptions(schoolNamesList);
    }

    private void SchoolValueNameChanged(TMP_Dropdown changeDropdown)
    {
        if (changeDropdown.value == 0 || changeDropdown.value == schoolNamesList.Count-1)
        {
            goBtn.SetActive(false);
        }
        else
        {
            goBtn.SetActive(true);
            loginManager._schoolName = schoolNamesList[changeDropdown.value];
        }
    }

}
